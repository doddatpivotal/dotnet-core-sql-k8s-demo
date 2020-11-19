# uses pack, but references builder created by TBS and stored in harbor
pack build employee-todo-list-api \
  --path employee-todo-list-api 
  # --builder harbor.sunspear.tkg-vsphere-lab.winterfell.live/tbs/build-service/default