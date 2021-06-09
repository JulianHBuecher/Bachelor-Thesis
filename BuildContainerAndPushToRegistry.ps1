function BuildContainer() {
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-identityserver:v1.0 -f .\IdentityServer\Dockerfile .\IdentityServer\
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-locationapi:v1.0 -f .\LocationApi\Dockerfile .\LocationApi\
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-ml.proxy:v1.0 -f .\ML.Proxy\Dockerfile .\
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-weatherapi:v1.0 -f .\WeatherApi\Dockerfile .\WeatherApi\
    docker build -t studentenk8sregistry.azurecr.io/jb-thesis-angular-webapp:v1.0 -f .\webapp\Dockerfile .\webapp\
}

function PushToRegistry() {
    az acr login --name studentenk8sregistry

    docker image push studentenk8sregistry.azurecr.io/jb-thesis-identityserver:v1.0 
    docker image push studentenk8sregistry.azurecr.io/jb-thesis-locationapi:v1.0 
    docker image push studentenk8sregistry.azurecr.io/jb-thesis-ml.proxy:v1.0
    docker image push studentenk8sregistry.azurecr.io/jb-thesis-weatherapi:v1.0 
    docker image push studentenk8sregistry.azurecr.io/jb-thesis-angular-webapp:v1.0 
}

if ($args[0] -eq "Build") {
    BuildContainer && PushToRegistry
} elseif ($args[0] -eq "Push") {
    PushToRegistry
}else {
    Write-Output "Wanted function is not available"
}