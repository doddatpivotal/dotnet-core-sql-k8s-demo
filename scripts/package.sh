# uses pack, but references builder created by TBS and stored in harbor
pack build employee-todo-list-api \
  --path employee-todo-list-api \
  --builder winterfell.azurecr.io/tbs/build-service/default@sha256:92b286fc049e58be05c2598f5cca1af7bd6ddd85c57c08932467d05b29497360