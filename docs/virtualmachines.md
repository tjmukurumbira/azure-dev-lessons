## Azure Virtual Machines

Compute available with or without operating system (Windows and Linux)

IaaS managed by Microsoft

Pay for running costs , can spin up or terminate any time

Disk Encyrption using keys stored in keyvault. Bitlocker 

```
az group create --name “demogrp” --location westus
az keyvault create --name "demovault2020" --resource-group "demogrp" --location westus
az keyvault key create --vault-name "demovault2020" --name "demokey" --protection software
az vm create  --resource-group “demogrp”  --name “demovm” --image win2016datacenter \
 --admin-username “demousr”  --admin-password “DemoPassword123”
az keyvault update --name "demovault2020" --resource-group "demogrp" --enabled-for-disk-encryption "true"
az vm encryption enable --resource-group “demogrp”  --name “demovm” --disk-encryption-keyvault "demovault2020" --key-encryption-key "demokey" --volume-type all
```

