# Transit Simulator
Transit Simulator is my take on a public transit simulator. It is meant to be a near-realistic simulation of transportation systems. Utilizing a complex AI that constantly optimizes itself on the fly, processing a dozen different metrics in real-time to provide the best-case micro-management of an entire transit line, this simulator hopes to allow users to evaluate the efficiency of different configurations, routes, and transportation methods across multiple lines spanning an entire network.

_If AI can do it better, so can we._

## Features
This lists the main features planned for the project:
- Core simulation engine
    - [ ] Different modes of transportation
        - [ ] Buses
        - [ ] Light Rail
        - [ ] Subways
        - [x] Commuter Rail
    - [ ] Realistic physics modeling for each vehicle type
        - [ ] Acceleration and deceleration
        - [ ] Maximum speeds
        - [ ] Turning radii
        - [ ] Real-world units mapped to in-game distances and speeds
        - [ ] Fuel consumption and emissions
        - [ ] Maintenance requirements
    - [ ] Infrastructure elements
        - [ ] Roads
        - [ ] Rail tracks
        - [ ] Stations and stops
        - [ ] Signaling systems
    - [ ] Line and route management
        - [ ] Route creation and editing
        - [ ] Schedule creation and editing
        - [ ] Vehicle assignment to routes
        - [ ] Stop placement and configuration
        - [ ] Cost management (budget constraints, vehicle fleet limits, etc.)
        - [ ] AI configuration (efficiency focus for cost saving, passenger satisfaction, or balanced)
    - [ ] AI system (the following are only for trains at the moment)
        - [ ] Real-time vehicle tracking
        - [ ] Automatic separation management
        - [ ] Automatic station stopping and departure
        - [ ] Dynamic allocation of vehicles based on demand
    - [ ] Passenger simulation
        - [ ] Boarding and alighting behavior
        - [ ] Route choice based on shortest time, least transfers, etc.
        - [ ] Demand simulation based on time of day, location, and other factors
        - [ ] Satisfaction metrics (wait times, crowding, etc.)
        - [ ] Ticket pricing simulation
    - [ ] Traffic simulation
        - [ ] Interaction with other vehicles (for buses)
        - [ ] Traffic signal priority for transit vehicles
        - [ ] Traffic light timing simulation
        - [ ] Congestion simulation
        - [ ] Weather impact simulation
- 2D User interface
    - [ ] Basic city layout editor
    - [ ] Route and schedule editor
    - [ ] Real-time vehicle tracking
    - [ ] Passenger load visualization
    - [ ] Extensive configuration options
        - [ ] AI behavior settings
        - [ ] Simulation speed controls
        - [ ] Visualization options
        - [ ] Physics model tuning
    - [ ] Performance metrics dashboard
        - [ ] Average wait times
        - [ ] Vehicle utilization rates
        - [ ] On-time performance
        - [ ] Operational costs

Many more features are planned but not listed here. All planned features can be found [here](https://github.com/smallketchup82/TransitSimulator/issues?q=is%3Aissue%20state%3Aopen%20label%3Atype%3Afeature).

## Development
This project is currently in the (very) early stages of development. As a result, we do not have release builds or binaries available yet. If you're interested in seeing where we're at right now, or want to contribute to the project, feel free to check out the repository and build it from source via the instructions in [CONTRIBUTING.md](CONTRIBUTING.md).

Right now, we use the Monogame engine for development. If you're interested in being a frequent contributor, shoot me an email at ketchup@smkt.ca and I'll be happy to drop you my Discord info so we can discuss the project further. I would very much appreciate any help I can get.

## Purpose
The primary purpose of this project is to serve as a proof of concept, to show that AI and algorithms can enhance many of the problems faced in public transit systems today.

I got the idea for this project while riding the GO Train in Toronto. I'm a frequent user of public transit, as I don't have my drivers license yet, nor am I really looking forward to shelling out the money for car insurance. I depend on public transit to get around, and much more often than not, I find myself frustrated with the current state of public transit systems. Whether it's delays, overcrowding, or inefficient routes, there are many areas where public transit can be improved. I started thinking: what if we just strapped an airtag onto every single transit vehicle to track their location, used real-time data to monitor their location, status, peformance, passenger loads, projected demand, and other factors. Then use AI to optimize the entire system in real-time?

What if we could create a transit system that is so efficient, so responsive, that it feels like a personal chauffeur service, but for the masses? No longer would you need to shell out $15 on an Uber because the next bus comes in 45 minutes. No longer would you need to cram like a sardine onto trains that arrive once an hour. No longer would you need to deal with the frustration of delays and cancellations. What if public transit could be so good, that it becomes the preferred mode of transportation for everyone? I want to explore these ideas and see if they can be turned into reality, with this project serving as the testing ground.

### Why not just use Cities: Skylines?
Don't get me wrong, I love the transit systems in Cities: Skylines 2, but I wanted to create something focused solely on transit systems, without the distractions of city-building mechanics. The simulator being 2D is also a deliberate choice to keep things simple and focused on the core mechanics of transit simulation.

Also, Cities: Skylines 2 isn't the most true-to-life simulator when it comes to transit systems. While it does a good job of simulating the general aspects of transit, it doesn't capture the full complexity and nuance of real-world transit systems. This project aims to fill that gap by providing a more detailed and accurate simulation of public transit, bringing in serious physics modeling to represent how different vehicles behave. I also want to make the AI much more advanced, allowing it to take into account a wider range of factors when making decisions, having it make real-time adjustments and micro-optimizations to improve efficiency and passenger experience.

### Goals
In a nutshell, I want to make an AI that can micromanage a transit system better than humans ever could. I want to show how:
- You don't need to cut off service between 3pm and 7pm to reserve the entire line for the PM rush hour, just because your line is single-tracked.
- You also don't have to run trains once per hour for the entire day for the same reason.
- You can have dynamic scheduling that adjusts in real-time based on various factors. Your busses shouldn't be coming and leaving 10 minutes before trains arrive at your station
- You don't need to have your busses scheduled 60 minutes apart in the suburbs. Infrequent service is genuinely a thing of the past.
- You don't even need to design routes in general. Let the AI use its algorithms to design the most efficient routes based on demand, operational costs/constraints, and other factors.
- You can turn the TTC streetcars into a genuinely good transit option instead of a slow, overcrowded mess, by optimizing their routes, schedules, capacities, stop placements, signal priorities, and more.

All of the stuff I listed above are genuine problems I've had to deal with while using public transit for years. I want to take things into my own hands to see if AI can do a better job at designing and managing transit systems than humans can. I want to allow other people to experiment with different designs and strategies for public transit systems, and see how they perform in a simulated environment. Want to see if a bus rapid transit system can outperform a light rail system in a given scenario? Want to see how different scheduling strategies affect passenger wait times and overall system efficiency? Want to see if bus lanes actually work? I hope to provide the answers with this project.

I don't want to stop at just that though, I also want to explore cost optimization and environmental impact. I want to allow the AI to optimize for different goals, whether it's minimizing costs, reducing emissions, or maximizing passenger satisfaction. I want to create the most advanced public transit simulator out there, and I want to do it in a way where anyone (whether its users or companies) can create different scenarios and evaluate their efficacy.

## License
This project is licensed under the GNU General Public License v3.0. See [LICENSE](LICENSE) for more details.
