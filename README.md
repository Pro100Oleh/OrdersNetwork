# OrdersNetwork

## About
This is C# demo with my blockchain implementation

## Requirements
* A distributed blockchain network to allow communicate 3 different types of users
  * Order makers: user publishes an order for execution. Sends to network a message about new order with Payment property.
  * Order executors: users that listen for new orders on network and peek up some of them for execution. Sends to network a message about intention to execute list of desired orders and Fee.
  * Network nodes that support this blockchain. Responsible to keep orders and order assignments in consistent state.
* Blockchain should support different kinds of orders – it should be pluggable.
* Demo contains plugin to support WalkDog orders that describe a walk dog task with properties like dog info, _StartTime_ and _Location_.

## What is not part of this demo
* No persistency: there are only in-memory blockchain objects support
* No authentication: all members rely on client name (string)
* No encryption: to simplify transactions/blocks verification used only hashes without asymmetric encryption
* No smart blocks synchronization: all blockchain members initialized in one time and can see only current new blocks in network. There is only one server node that produce a new blocks.

## How it works
* One or several users publish message _OrderRequested_ with a new order to network
* Server listens to this new orders and add them to own _ongoing_ block – it is next block candidate.
* At the same time server tries to sign _ongoing_ block. When next block has a signature (hash value of _ongoing_ block) that in less than configuration value _HashFactor_ - then a new block will be added to blockchain. _HashFactor_ responsible for probability that a new block will be included.
* Server publishes a new block to all members. Clients update their own copy of blockchain.
* Now another users can see in blockchain a new unassigned orders. They publish another type of messages (_OrdersAssigned_) to network with list of orders desired for execution. Also add a _Fee_ parameter – what part of payment will take a server.
* Server keeps _OrdersAssigned_ messages on the same _ongoing_ block. A new message will be added to _ongoing_ block only if it not break blockchain consistency. Consistency of _OrderRequested_ messages depends from order type and as a result – from referenced plugin. Consistency of _OrdersAssigned_ - can be only one executor of order. Because many executors can publish a message with same order – server will choose that which gives him the best prize – calculated as _Fee_*_Payment_. To simplify general consistency validation is used one limitation: each next block can contains only one message from one user.
* Who is real order executed calculated by next signed blocks: server should include a _OrdersAssigned_ message to new block.
* When clients receive next block with their message inside – then they can publish new messages.
