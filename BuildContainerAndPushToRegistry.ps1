function BuildContainer() {
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-identityserver:v2.0 -f .\IdentityServer\Dockerfile .\IdentityServer\
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-locationapi:v2.0 -f .\LocationApi\Dockerfile .\LocationApi\
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-ml.proxy:v3.4 -f .\ML.Proxy\Dockerfile.Production .\
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-weatherapi:v2.1 -f .\WeatherApi\Dockerfile .\WeatherApi\
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-angular-webapp:v2.0 -f .\webapp\Dockerfile .\webapp\
}

function PushToRegistry() {
    az acr login --name studentenk8sregistry

    docker image push studentenk8sregistry.azurecr.io/jb-thesis-identityserver:v2.0 
    docker image push studentenk8sregistry.azurecr.io/jb-thesis-locationapi:v2.0 
    docker image push studentenk8sregistry.azurecr.io/jb-thesis-ml.proxy:v3.4
    docker image push studentenk8sregistry.azurecr.io/jb-thesis-weatherapi:v2.1 
    docker image push studentenk8sregistry.azurecr.io/jb-thesis-angular-webapp:v2.0 
}

if ($args[0] -eq "Build") {
    BuildContainer && PushToRegistry
} elseif ($args[0] -eq "Push") {
    PushToRegistry
}else {
    Write-Output "Wanted function is not available"
}