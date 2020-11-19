# Employee TODO List app

This is a todo demo application.  It is built with an Angular front end, .NET Core 3 api, and a MS Sql Server database.

The rationale for this demo is to demonstrate, how to build and deploy an app with these technologies to Kubernetes.

## Deploy the app only

During my development, I have containerized the application and published the container image to Docker Hub.  As such, you can directly deploy the app to your Kubernetes cluster without building it.

>Note: Requires Carvel tools [kapp](https://get-kapp.io) and [ytt](https://get-ytt.io)

You can customize for your environment by editing `k8s/values.yaml`.  Definitely do this if you are using the ingress.

```bash
kubectl create ns todos
ytt --ignore-unknown-comments -f k8s/ | kapp deploy -n bizops -a todos -y -f -
```

If you have used the ingress, then access the app via the ingress URL.  Alternatively, you can access it through the todo Loadbalancer service.

## Thank You ultrasonicsoft

Thank you to https://github.com/ultrasonicsoft/employee-todo-list.git for the majority of the source code for this project and the basis for my own learning.
