## Containers
Containers are standalone units of software , that contain only software code and what is required to run the code. Docker to manage containers and  dockerhub repository for container images.  Azure container registry

Docker files  have instructions on how to create container image
Docker file
```
#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["src/dockerwebbapp/dockerwebbapp.csproj", "src/dockerwebbapp/"]
RUN dotnet restore "src/dockerwebbapp/dockerwebbapp.csproj"
COPY . .
WORKDIR "/src/src/dockerwebbapp"
RUN dotnet build "dockerwebbapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dockerwebbapp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dockerwebbapp.dll"]

```


```
az acr create --resource-group azuredemo --name demoregistry2020 --sku Basic

Push an image
sudo az acr login --name demoregistry2020

sudo docker tag demoapp demoregistry2020.azurecr.io/azuredemoapp

sudo docker push demoregistry2020.azurecr.io/azuredemoapp
```

## Scaling kubernetes

```
az group create --name kubernetesgrp --location eastus

az aks create --resource-group kubernetesgrp --name democluster --node-count 1 --enable-addons monitoring --generate-ssh-keys

az aks install-cli --install-location=./kubectl

az aks get-credentials --resource-group kubernetesgrp --name democluster 
```

1. Scale Pods using command
    ```
    kubectl scale --replicas=3 deployemnt/az203kube
    ````
2. Scaling Nodes
    ```
    az aks scale --resource-group az203 --name az203kube --node-count 3
    ```

Deploy to kubernetes cluster using yaml file
```
AKS_RESOURCE_GROUP=kubernetesgrp
AKS_CLUSTER_NAME=democluster
ACR_RESOURCE_GROUP=azuredemo
ACR_NAME=demoregistry2020


CLIENT_ID=$(az aks show --resource-group $AKS_RESOURCE_GROUP --name $AKS_CLUSTER_NAME --query "servicePrincipalProfile.clientId" --output tsv)

ACR_ID=$(az acr show --name $ACR_NAME --resource-group $ACR_RESOURCE_GROUP --query "id" --output tsv)

az role assignment create --assignee $CLIENT_ID --role acrpull --scope $ACR_ID

kubectl apply -f app.yml

kubectl apply -f service.yml
```