# Employee TODO List app

This is a todo demo application.  It is built with an Angular front end, .NET Core 3 api, and a MS Sql Server database.

The rationale for this demo is to demonstrate, how to build and deploy an app with these technologies to Kubernetes.

## Deploy the app only

During my development, I have containerized the application and published the container image to Docker Hub.  As such, you can directly deploy the app to your Kubernetes cluster without building it.

>Note: Requires Carvel tools [kapp](https://get-kapp.io) and [ytt](https://get-ytt.io)

Make a copy of `local-config/values-REDACTED.yaml` and customize for your environment.  Then set the environment variable `PARAMS_YAML` to the location of your copied file.  Mine is `local-values/values.yaml` which is in the `.gitignore`.

```bash
export PARAMS_YAML=local-config/values.yaml
NAMESPACE=$(yq e .todosApp.namspace $PARAMS_YAML)

kubectl create ns $NAMESPACE -oyaml --dry-run=client | kubectl apply -f -

ytt --ignore-unknown-comments -f k8s/ -f $PARAMS_YAML| kapp deploy -n $NAMESPACE -a todos -y -f -
```

If you have used the ingress, then access the app via the ingress URL.  Alternatively, you can access it through the todo Loadbalancer service.

## Load Runner

1. Run locust via docker

```bash
./scripts/load.sh
```

2. Access Locus UI

```bash
open http://localhost:8089

```

## Thank You ultrasonicsoft

Thank you to https://github.com/ultrasonicsoft/employee-todo-list.git for the majority of the source code for this project and the basis for my own learning.
