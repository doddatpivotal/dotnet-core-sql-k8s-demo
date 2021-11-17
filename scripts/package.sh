# uses pack, but references builder created by TBS and stored in harbor
pack build employee-todo-list-api \
  --path employee-todo-list-api \
  --builder winterfell2.azurecr.io/tbs:clusterbuilder-base@sha256:1b1541e3c3c25353e291b95de431b756eba19f704d41c92b263139a495bce0e6