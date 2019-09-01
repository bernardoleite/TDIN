# TDIN - Distribution and Integration Technologies

## TDIN1 - Intranet Distributed Application (using .NET Remoting: Restaurant order and account system
### Scenario
A restaurant needs to automatize the dining room orders, allowing them to be quickly communicated to the kitchen and bar tenders, to be prepared as soon as possible. Also, the same system should maintain a complete record of all orders, compute the bills of each table, and maintain a record of the total amount received in the day. In the dining room there are several terminals (client computers) where the waiters can make new orders (in any of the terminals), opening a new table or adding to the orders already assigned to a table. Those orders can have as destination the kitchen (dishes) or the bar (drinks). When the orders become prepared, cooks and bar tenders should signal the event, letting the waiters know that they can fetch the orders and serve them to the tables. In the bar area and the kitchen there is also a terminal where the orders should appear, already divided (bar orders in the bar terminal and kitchen orders in the kitchen terminal). When a free bar tender or cook picks one order to prepare it, he should signal that in the terminal changing its status from ‘not picked’ (the original status of any order) to ‘in preparation’, preventing other person to prepare the same order.
When the order is prepared (drink or dish) the same person should go to the terminal (bar or kitchen) and change its status to ‘ready’, and that should be signaled or shown in the dining room terminals.

### Implementation
This system of applications and functionalities should be written using .NET, with a set of methods and events in remote objects in .NET Remoting. The user interfaces should be graphic (GUI, using for instance WinForms) but simple and intuitive to use. The application simulating the printer could be textual (console). You can always include other functionalities considered useful (will be valued) and implement everything using the best practices for the technologies in place.

## TDIN2 - An enterprise distributed system
### Scenario
A book editor prints and sells books in his own installations and book shop. It intends to develop a system for coordinating its sells, orders and stock management. The editor owns two facilities physically different, the bookshop store with a public exposition area, and a warehouse for storing larger quantities of books. The editor intends also to make available a web application for remote consulting and ordering. In the bookshop store a web server is running, hosting the web application, and some persistent record of available titles, price and existences (stock) in the bookshop and warehouse. Some services can also be hosted in the same server. This server is always on and is connected to the internet. Between the store and warehouse there is also a network connection, but the warehouse computers usually run only in labor hours, including its own server.

### Implementation
Implement this distributed application following the Service Oriented Architecture (SOA) principles. You can choose any appropriate technologies, but they need to include remote services (XML or REST), a queue for not loosing requests to the warehouse, and a dual service (or equivalent) for ‘calling’ the printer (which is simulated by a client application) from the service at the store.

The service at the store server should support several operations like:
* consult the store stock of a title
* make a sell (updates the store stock and prints a receipt)
* create an order
* change the state of an order
* any other convenient operations

The service at the warehouse should support operations like:
* get requests that arrived from the store
* complete a request
* any other convenient operations

The information needed to the servers in the application can be persisted in files or local databases.
