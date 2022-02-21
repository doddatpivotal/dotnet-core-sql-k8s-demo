: ${PARAMS_YAML?"Need to set PARAMS_YAML environment variable"}

# delete and recreate
NAMESPACE=$(yq e .todosApp.namespace $PARAMS_YAML)
kubectl delete deployment todos -n $NAMESPACE
ytt --ignore-unknown-comments -f k8s/ -f $PARAMS_YAML | kapp deploy -n $NAMESPACE -a todos -y -f -
