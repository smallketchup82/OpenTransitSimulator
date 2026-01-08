# Open Transit Simulator
Open Transit Simulator is my take on a public transit simulator. It is meant to be a near-realistic simulation of transportation systems. Utilizing a complex AI that constantly optimizes itself on the fly, processing a dozen different metrics in real-time to provide the best-case micro-management of an entire transit line, this simulator hopes to allow users to evaluate the efficiency of different configurations, routes, and transportation methods across multiple lines spanning an entire network.

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
This project is currently in the (very) early stages of development. As a result, we do not have release builds or binaries available yet. If you're interested in seeing where we're at right now, or want to contribute to the project, feel free to read [CONTRIBUTING.md](CONTRIBUTING.md).

### Roadmap
The following are my estimated durations for how long it will take to implement certain features, and the overall timeline of when certain features are expected to be integrated.

| Milestone                        | Description                                                                                                                                                                                                                                                                                                                                                             | Duration | ETA (Sum of Duration) |
|----------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------|-----------------------|
| Game Engine Completion           | Addressing everything in https://github.com/smallketchup82/TransitSimulator/issues/12                                                                                                                                                                                                                                                                                   | 4 months | 4 months              |
| Simulation Foundation Completion | Adding in the pieces for the simulation.<br>Roughly translates to: adding locomotives, cars, rails, stations, switches, blocks & signals                                                                                                                                                                                                                                 | 2 months | 6 months              |
| Simulation Feature Completion    | Adding in the final pieces and making everything work. Roughly translates to:<br><br>- Making locomotives follow rails, and switching rails at switches<br>- Making carriages/cars follow locomotives<br>- Adding in physics engine (incl. collision detection)<br>- Adding in the AI (incl. pathfinding)<br>- Adding in rail yards<br>- Adding in passenger simulation | 4 months | 10 months             |
| User Interface Completion        | Adding in a user interface, statistics, labels & details when hovering over trains, etc.                                                                                                                                                                                                                                                                                | 2 months | 12 months             |

## Purpose
This project has an interesting purpose behind it, with a lot of thought put into the reasoning for creating it in the first place. To read more about why this project exists, what its goals are, what it hopes to prove, and what it hopes to achieve, check out [PURPOSE.md](PURPOSE.md).

## License
This project is licensed under the [GNU Lesser General Public License v3.0](COPYING.LESSER). In general, if you want to copy the codebase, making your own tweaks for your own personal use, while keeping your modified version closed source, that's completely fine! But if you plan on redistributing your modified version, we want your version to be open source. Feel free to contact me at ketchup@smkt.ca for any questions or concerns.
