: ${PARAMS_YAML?"Need to set PARAMS_YAML environment variable"}

TAG=$(yq e .todosApp.apiContainerImage $PARAMS_YAML)
docker tag employee-todo-list-api $TAG
docker push $TAG

TAG=$(yq e .todosApp.appContainerImage $PARAMS_YAML)
docker tag employee-todo-list-app $TAG
docker push $TAG
