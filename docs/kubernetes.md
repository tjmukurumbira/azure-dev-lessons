## Scaling kubernetes

1. Scale Pods using command
    ```
    kubectl scalee --replicas=3 deployemnt/az203kube
    ````
2. Scaling Nodes
    ```
    az aks scale --resource-group az203 --name az203kube --node-count 3
    ```

