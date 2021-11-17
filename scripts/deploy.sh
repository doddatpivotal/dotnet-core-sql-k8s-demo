kubectl delete deployment todos -n todos
ytt --ignore-unknown-comments -f k8s/ | kapp deploy -n todos -a todos -y -f -
