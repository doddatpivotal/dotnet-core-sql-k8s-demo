: ${PARAMS_YAML?"Need to set PARAMS_YAML environment variable"}

# uses pack, but references builder created by TBS and stored in harbor
pack build employee-todo-list-api \
  --path employee-todo-list-api \
  --builder $(yq e .todosApp.cluster_builder $PARAMS_YAML)