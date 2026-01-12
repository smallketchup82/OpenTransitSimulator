# Contributing Guide
Thank you for your interest in contributing to this project! We welcome contributions from the community and appreciate your efforts to help improve our codebase. Please follow the guidelines below to ensure you're on board with our contribution process.

## Reporting Issues
If you encounter a bug, please open an issue on our GitHub repository. Note that we define "issues" as a situation where something is clearly not working as intended (e.g. text overlapping, crashes, slowdowns in specific scenarios, etc.)

When reporting an issue, please do the following:
- Search existing issues to see if the problem has already been reported.
- When creating the issue, please provide a clear and concise description of the problem, steps to reproduce it, and any relevant screenshots or logs.
- Include any relevant information about your environment (e.g., operating system, game version, any relevant hardware details).
- List any steps to reproduce the issue, and any steps you've already taken to try to resolve it. Let us know what worked and what didn't, and help us try to narrow down the cause.

If the issue isn't reproducible or lacks sufficient detail, we may ask for additional information or close the issue until more details are provided. If the issue is confirmed to be specific to your setup, we will convert your issue into a discussion to help you troubleshoot.

Also, if you feel something is off, could be improved, or missing, please open a discussion instead of an issue. We also recommend discussions if you're unsure whether something is a bug or intended behavior.

## Providing Feedback
As the project is in the early stages of development, any feedback is highly appreciated. If you don't like the way something looks, feels, or works, let us know! If you have an idea for something new, please share it! Your feedback will help shape the direction of the project, and allow us to prioritize what to work on next.

When providing feedback, please mind the following:
- As with issues, please search existing discussions to see if your feedback has already been shared.
- Be respectful and constructive in your feedback. Remember that there are real people behind the project who are putting in time and effort to make it happen. Make sure your feedback is constructive, and doesn't just criticize a feature or design without offering suggestions for improvement.
- Examples, mockups, designs, or references can be very helpful in conveying your ideas and suggestions.
- Try to keep feedback realistic and actionable. While we appreciate ambitious ideas, it's important to consider the feasibility and impact of your suggestions. No matter how good an idea sounds, if it requires weeks of work to implement, it will certainly not be prioritized in the near future.
    - If we don't act on or respond to your feedback right away, please don't take it personally. We may need time to evaluate and prioritize it. There's also the chance that it may not align with our current goals.

You can provide feedback through GitHub Discussions. We will always try to respond to feedback in a timely manner, but given that smallketchup82 is currently the sole maintainer, please be patient if responses take longer than expected.

## Setting up Your Development Environment

### Prerequisites
You'll need the following tools installed on your machine to start:
- An IDE made for C# development
  - We recommend using [JetBrains Rider](https://www.jetbrains.com/rider/) as it comes with run configurations for the project. However, any IDE made for .NET development, with editorconfig support, should work fine.
  - [Visual Studio](https://visualstudio.microsoft.com/), or [VSCode](https://code.visualstudio.com/) with the [C#](https://code.visualstudio.com/docs/languages/csharp) and [editorconfig](https://marketplace.visualstudio.com/items?itemName=EditorConfig.EditorConfig) extensions are good alternatives.
- [.NET SDK 10.0 or later](https://dotnet.microsoft.com/en-us/download)
- [Git](https://git-scm.com/)
  - We recommend using [GitHub Desktop](https://github.com/apps/desktop) for its low barrier of entry, but any Git client will work.
  - Ensure you have configured your Git identity, and have a decent understanding of how both Git and GitHub work.
  - Useful Git resources: [Pro Git](https://git-scm.com/book/en/v2), [Git Immersion](https://gitimmersion.com/), [GitHub Flow](https://docs.github.com/en/get-started/using-github/github-flow) (this is more about GitHub and collaboration), and [more](https://docs.github.com/en/get-started/start-your-journey/git-and-github-learning-resources).

### Cloning the Repository
Clone the repository to your local machine using Git. You can do this easily via GitHub Desktop, or by running the following command in your terminal:
```bash
git clone https://github.com/smallketchup82/OpenTransitSimulator
```

### Building & Running the Project
Open the solution file (`OpenTransitSimulator.sln`) in your IDE of choice. Restore the NuGet packages (`dotnet restore` if your IDE doesn't do this automatically), and build the solution (`dotnet build` via the CLI).

To run the project, you can use our custom run configurations in JetBrains Rider, or run the following command in your terminal:
```bash
dotnet run --project OpenTransitSimulator
```

Hopefully, the project should build and run without any issues. If you encounter any problems during setup, please open a discussion on GitHub for assistance.

## Contributing Code

### What to Expect
- At the moment, we are focused on building the core simulation engine. A lot of what we're working on is foundational, and may not be easily digestible for new contributors. Ensure you have a good understanding of at least the following before you dive in:
  - [C# and .NET development](https://learn.microsoft.com/en-us/dotnet/csharp/)
  - [Object-oriented programming principles](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/)
  - [Basic game development concepts](https://gameprogrammingpatterns.com/contents.html)
- Over time, as we add more features, components, and abstractions, the codebase will become a lot more approachable for new contributors. If you're unsure where to start, please reach out via discussions!

### Guidelines
- Make sure to fork the repository, and **create a new branch for each feature or bug fix you work on**.
- **Do not submit pull requests without prior discussion.** Any pull requests opened without a linked issue or discussion will be automatically closed. Random pull requests create a lot of confusion (and frustration) that we simply aren't willing to deal with.
  - Adding onto this, we ask that before starting work on a new feature or bug fix, you send a message in the relevant issue or discussion threads to let everyone know that you're working on it. This way, we can provide advice, guidance, and let you know if someone else is already working on it. This helps avoid wasted effort and gives us less work to do during the review process.
- Try to follow the existing coding style and conventions used in the project.
- Keep your commits neatly organized and descriptive. Use clear commit messages that explain the purpose of each change. And avoid creating pull requests with a large amount of small commits. We recommend squashing commits into bigger logical changes before submitting a pull request.
- Write tests for your changes where applicable. This helps ensure that your code works as intended and prevents regressions in the future. It also makes it easier for us to review your changes.
- Do not make changes through the GitHub web interface. Always work with an IDE.
- Make sure to run any code formatters, linters, and tests before opening your pull request.
- Develop your features in the `Debug` configuration unless you are specifically working on performance optimizations or release-specific changes. The `Debug` configuration provides better debugging, logging, and error reporting which is essential during development.
- Document your code where necessary. Use XML documentation comments for public methods, classes, and properties to explain their purpose and usage. Use inline comments sparingly to clarify complex logic. Avoid adding obvious comments that do not add value, such as comments for straightforward private methods.
- Don't continually update your branch with the `main` branch. Rebase your branch onto `main` before opening a pull request, and only update it if there are merge conflicts or significant changes in `main` that affect your work. We will handle merging `main` into your branch during the review process if necessary.
- While we appreciate all contributions, please be aware that not all pull requests will be accepted, and not all pull requests will be merged smoothly. We may give you critical feedback, request significant changes, or even toss out your approach entirely. Please don't take this personally; we never try to equate code with contributors. Our goal is to ensure that each change aligns with the project's goals, maintains code quality, and fits well within the existing architecture, all while being maintainable in the long term. Take any feedback, critique, or rejection as a learning opportunity. 
- It goes without saying, but ensure your pull request's body has a detailed explanation of your changes. If in doubt, use the following as a checklist:
  - Are there any pre-requisites for these changes? (list any additional pull requests that need to be merged first)
  - What problem does this PR solve?
  - What changes were made? (additionally provide screenshots, videos, or GIFs if applicable)
  - Why did you choose this approach? How is this the best solution to the problem?
  - How can these changes be tested?
  - Are there any side effects or risks associated with these changes?
  - Any additional notes, resources, links, or context that reviewers should be aware of.

Remember, ASK for help if you need it (even if you think you don't)! If you're unsure about something, need guidance on how to proceed, or want to know if you're on the right path, PLEASE ask in the relevant issue or discussion thread. Making assumptions and creating pull requests out of the blue is rightfully going to result in us rejecting your work and telling you off. We want collaboration, not chaos. Spending 30 seconds answering a question is better than spending 30 minutes trying to make sense of a pull request we had no idea was coming.

### Choosing What to Work On
Figuring out what to work on can be tricky, especially now that the project is still in its early stages. Here are some tips to help you decide:
- Start by browsing the open issues and discussions on GitHub. Look for issues that are labeled as "good first issue" if you're new to the project. These issues are typically smaller in scope and easier to tackle.
- Issues labeled as "help wanted" should be great for more experienced contributors. These issues are usually very complex, require a deep understanding of the codebase, need significant design work, and are oftentimes high priority. Helping out with these issues when you're able to is a great way to make meaningful contributions that are almost guaranteed to be merged.
- Issues are marked by priority labels. Going for high priority issues is a good way to find bigger problems that need solving, and have a good chance of getting quick attention from reviewers.
- Otherwise, feel free to focus on areas of the codebase that interest you the most. If you like an idea or feature that has been proposed and gotten the green light by maintainers, go for it!
- If you're still unsure, you can always reach out to us via GitHub discussions or email (ketchup@smkt.ca). We can try to suggest issues that match your skills, interests, and the project's needs.

## Getting More Involved (Beyond Just Code)
If you're interested in regularly contributing to the project, or want to help in other ways beyond just writing code, please reach out to me (smallketchup82) at ketchup@smkt.ca. I'm always open to discussing opportunities for collaboration and letting people help out more directly. Whether that is helping with dependencies, documentation, triage, planning, design, or anything else. I'm always open to having more people involved and actively making progress on the project.

I'm fully open to mentorship and helping people learn. If you're interested in contributing code but unsure where to start, please reach out! I'm happy to help guide you through the process, suggest beginner-friendly issues, and provide feedback on your work. I'm also open to explaining concepts, helping you learn Git & C#, and generally being a resource for you as you get started (within reason of course).

My hope is that Open Transit Simulator can become a passion project not just for me, but for a community of people striving for the same goal. Please don't hesitate to get in touch, I want to help you to help me!

## Resources & Documentation
- The [Wiki](https://github.com/smallketchup82/OpenTransitSimulator/wiki) section of our GitHub repository can contain useful information about different parts of the project, such as a breakdown of the math behind the AI system, and diagrams explaining the architecture of various components.
- Our project is built on top of MonoGame. The [MonoGame Documentation](https://docs.monogame.net/) is a great resource for understanding how to work with the engine, and can help you get up to speed quickly.
- Microsoft's [C# documentation](https://learn.microsoft.com/en-us/dotnet/csharp/) is an invaluable resource for learning the language and its features.
- A discord server for development may be considered if the project gains enough traction and contributors. For now, we will be sticking to GitHub for all communication.

Thank you for taking the time to read through our contributing guide. We look forward to seeing your contributions, and working together to develop a Free and Open-Source Transit Simulator!
