: ${PARAMS_YAML?"Need to set PARAMS_YAML environment variable"}

pack build employee-todo-list-api \
  --path employee-todo-list-api \
  --builder $(yq e .todosApp.cluster_builder $PARAMS_YAML)

pack build employee-todo-list-app \
  --path employee-todo-list-app \
  --env BP_NODE_RUN_SCRIPTS=build \
  --env BP_WEB_SERVER=nginx \
  --env BP_WEB_SERVER_ROOT=dist \
  --builder $(yq e .todosApp.cluster_builder $PARAMS_YAML)
