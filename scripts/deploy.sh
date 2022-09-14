: ${PARAMS_YAML?"Need to set PARAMS_YAML environment variable"}

# delete and recreate
NAMESPACE=$(yq e .todosApp.namespace $PARAMS_YAML)
ytt --ignore-unknown-comments -f k8s/ -f $PARAMS_YAML | kapp deploy -n $NAMESPACE -a todos -y -f -
