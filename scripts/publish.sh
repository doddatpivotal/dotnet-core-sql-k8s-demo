: ${PARAMS_YAML?"Need to set PARAMS_YAML environment variable"}

TAG=$(yq e .todosApp.containerImage $PARAMS_YAML)
docker tag employee-todo-list-api $TAG
docker push $TAG
