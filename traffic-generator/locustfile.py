# This program will generate traffic for ACME Fitness Shop App. It simulates both Authenticated and Guest user scenarios. You can run this program either from Command line or from
# the web based UI. Refer to the "locust" documentation for further information. 

from locust import HttpUser, TaskSet, task, User, between
import random
import logging
from random import seed
from random import randint


class UserBehavior(TaskSet):

    def on_start(self):
        self.home()

    @task(14)
    def home(self):
        logging.info("Accessing Home Page")
        self.client.get("/")

    @task(15)
    def findOwners(self):
        logging.info("Finding Employees")
        self.client.get("/api/employees")

    @task(13)
    def accessEmployees(self):
        logging.info("Accessing Employees Page")
        self.client.get("/index.html")

    @task(1)
    def createTodo(self):
        logging.info("Creating TODO")
        # generate random integer values
        # seed random number generator
        seed(1)
        # generate some integers
        value = randint(0, 10)
        myheaders = {'Content-Type': 'application/json', 'Accept': 'application/json'}

        employee = {"title": "Tackle runner " + str(value), "employeeId": "1"}
        self.client.post("/api/todos", json=employee)

    @task(1)
    def genError(self):
        logging.info("Generating Error")
        self.client.get("/api/employees/700")

class WebSiteUser(HttpUser):

    tasks = [UserBehavior]
    wait_time = between(1, 5)



