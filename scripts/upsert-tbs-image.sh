export TBS_NAMESPACE=todos
export IMAGE_NAME=todos
# export LOCAL_PATH=employee-todo-list-api/bin/Debug/netcoreapp3.1/employee-todo-list-api.dll
export LOCAL_PATH=employee-todo-list-api/
export IMAGE_REPOSITORY=winterfell.azurecr.io/todos/todos
export REGISTRY=winterfell.azurecr.io
export REGISTRY_USER=demo-token
export REGISTRY_PW=jW/9dg6dQYhVesgSvhf8GeYvk9/jERKe

# kp secret create harbor-creds \
#   --registry $REGISTRY \
#   --registry-user $REGISTRY_USER \
#   --namespace $TBS_NAMESPACE

set +e
kp image list -n $TBS_NAMESPACE | grep $IMAGE_NAME 
exists=$?
set -e
if [ $exists -eq 0 ]; then
  kp image patch $IMAGE_NAME \
    --namespace $TBS_NAMESPACE \
    --wait \
    --local-path $LOCAL_PATH
else
  kp image create $IMAGE_NAME \
    --tag $IMAGE_REPOSITORY \
    --namespace $TBS_NAMESPACE \
    --wait \
    --local-path $LOCAL_PATH
fi