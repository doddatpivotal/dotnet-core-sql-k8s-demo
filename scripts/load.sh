: ${PARAMS_YAML?"Need to set PARAMS_YAML environment variable"}

docker run -p 8089:8089 -v $PWD:/mnt/locust locustio/locust -f /mnt/locust/traffic-generator/locustfile.py -H http://$(yq e .todosApp.ingressUrl $PARAMS_YAML) -u 5 -r 2