# Installation Script for Azure Kubernetes Cluster


@"
#                                   _____           _        _ _       _   _               
#       /\                         |_   _|         | |      | | |     | | (_)            
#      /  \    _____   _ _ __ ___    | |  _ __  ___| |_ __ _| | | __ _| |_ _  ___  _ __    
#     / /\ \  |_  / | | | '__/ _ \   | | | '_ \/ __| __/ _` | | |/ _` | __| |/ _ \| '_ \   
#    / ____ \  / /| |_| | | |  __/  _| |_| | | \__ \ || (_| | | | (_| | |_| | (_) | | | |
#   /_/    \_\/___|\__,_|_|  \___| |_____|_| |_|___/\__\__,_|_|_|\__,_|\__|_|\___/|_| |_|
#                                                                                        
#
#                                       #                                    
#                                    ###  ##*                                   
#                                 #####  #####                                  
#                               #####(  #######                                 
#                              #####   ##########                               
#                            ######      /########                              
#                           ######         .########                            
#                         #######             #######                           
#                                   /#################.
#
# Installation-Script by Julian Bücher            
"@


function Install() {
    
    Write-Output "`n`nChecking for Updated in the Helm Repo: "
    # helm repo update

    Write-Output "`n`nIf not exist, create the new Kubernetes namespace for project: "
    kubectl create namespace jb-thesis-project

    Start-Sleep -s 5
    Write-Output "`n`nInstallation of Nginx Ingress: "

    helm install nginx-ingress-jb-thesis ingress-nginx/ingress-nginx `
    --namespace jb-thesis-project `
    --set controller.nodeSelector."beta\.kubernetes\.io/os"=linux `
    --set controller.replicaCount=1 `
    --set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux `
    --set controller.service.loadBalancerIP="20.79.91.37" `
    --set controller.service.externalTrafficPolicy=Local `
    --set controller.service.annotations."service\.beta\.kubernetes\.io/azure-dns-label-name"="jb-thesisproject"
        

    Start-Sleep -s 5
    Write-Output "`n`nInstallation of ConfigMaps: "

    kubectl apply -f .\webapp-configmap.yml &&
    kubectl apply -f .\prometheus-config.yml &&
    kubectl apply -f .\grafana-config.yml &&
    kubectl apply -f .\project-configmap.yml &&
    kubectl apply -f .\redis-config.yml &&
    kubectl apply -f .\ml.proxy-configmap.yml

    Start-Sleep -s 5
    Write-Output "`n`nInstallation of Applications: "
    
    kubectl apply -f .\webapp-deployment.yml &&
    kubectl apply -f .\identityserver-deployment.yml &&
    kubectl apply -f .\weatherapi-deployment.yml &&
    kubectl apply -f .\locationapi-deployment.yml &&
    kubectl apply -f .\redis-deployment.yml &&
    kubectl apply -f .\ml.proxy-deployment.yml &&
    kubectl apply -f .\elasticsearch-kibana-deployment.yml &&
    kubectl apply -f .\prometheus-deployment.yml &&
    kubectl apply -f .\grafana-deployment.yml
    
    Start-Sleep -s 20
    Write-Output "`n`nInstallation of Namespace Issuer: "
    
    kubectl apply -f .\namespace-issuer.yml

    Start-Sleep -s 5
    Write-Output "`n`nInstallation of TLS Ingress: "
    
    kubectl apply -f .\tls-ingress-deployment.yml

    Write-Output "`n`nInstallation of ConfigMap for Nginx Ingress: "

    kubectl apply -f .\tls-ingress-configmap.yml

    Write-Output "`n`n"

    bash -c "cowsay -f tux Installation succeeded"
}

function Install-CertManager() {
    Write-Output "`n`nCreate Namespace for Cert-Manager: "
    kubectl create namespace cert-manager

    # Cert-Manager is a central instance in a cluster to issue certificates to ingresses
    # It's secrets are available via several namespaces in the whole cluster
    # Because of this, we deploy it in a separat namespace
    # See this for deletion: https://stackoverflow.com/questions/52369247/namespace-stuck-as-terminating-how-do-i-remove-it
    # For handling errors: https://github.com/jetstack/cert-manager/issues/2757

    Start-Sleep -s 10
    Write-Output "`n`nInstallation of Cert Manager: "
    
    helm install cert-manager `
      --namespace cert-manager `
      --version v1.3.1 `
      --set installCRDs=true `
      --set nodeSelector."beta\.kubernetes\.io/os"=linux `
      jetstack/cert-manager
}

function Uninstall() {
    Write-Output "`n`nUninstall all existing Services, Deployments and Ingresses: `n"
    kubectl delete -f .\manifests\namespace-issuer.yml
    kubectl delete -n jb-thesis-project svc,deployment,ingress,configmap --all

    Start-Sleep -s 5
    Write-Output "`n`nUninstall the Helm charts: `n"
    helm uninstall nginx-ingress

    bash -c "cowsay -f tux Removal succeeded"
}

function Uninstall-CertManager() {
    Write-Output "`n`nUninstall Helm Chart: "
    helm uninstall cert-manager
    
    bash -c "cowsay -f tux Removal succeeded"
}

if ($args[0] -eq "Install") {
    Install
} elseif ($args[0] -eq "Uninstall") {
    Uninstall
} elseif ($args[0] -eq "Reinstall") {
    Uninstall && Install
} else {
    Write-Output "Wanted function is not available"
}

