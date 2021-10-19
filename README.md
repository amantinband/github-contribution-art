<div align="center"> 

<img src="GithubContributionArt\Client\wwwroot\images\avatar.svg" alt="drawing" width="300px"/>

# GitHub Contribution Art

## Fill your contribution grid real estate with Pixel Art!

[![GitHub contributors](https://img.shields.io/github/contributors/mantinband/amiko-dark)](https://GitHub.com/mantinband/amiko-dark/graphs/contributors/) [![GitHub Stars](https://img.shields.io/github/stars/mantinband/amiko-dark.svg)](https://github.com/mantinband/amiko-dark/stargazers) [![GitHub license](https://img.shields.io/github/license/mantinband/amiko-dark)](https://github.com/mantinband/amiko-dark/blob/main/LICENSE)

</div>

---

GitHub Contribution Art is a web interface which can be used to create pixel art on the GitHub contribution grid 💪

---

## Setup

1. `clone` the repo.
2. Setup an OAuth application for testing locally:
   1. Settings -> Developer settings -> OAuth Apps -> New OAuth App
   2. Application name: whatever you wish
   3. Homepage URL: https://localhost:44353/
   4. Authorization callback URL: https://localhost:44353/authcallback
3. Generate a Client secret:
   1. Settings -> Developer settings -> OAuth Apps -> your app -> Generate a new client secret.
   2. Copy this secret somewhere since it won't be available later.
4. in `GithubContributionArt\Server\appsettings.json` replace `ClientId` with your client id and `ClientSecret` with your secret.
5. in `GithubContributionArt\Client\wwwroot\appsettings.json` replace `github-client-id` with your client id.

---

## Contributing

If you found a bug, have an idea, or want to fix an issue please open an issue and lets discuss! 💪

---

## Credits

- [LibGit2Sharp](https://github.com/libgit2/libgit2sharp) - Powerful git wrapper.
- [Blazored LocalStorage](https://github.com/Blazored/LocalStorage) - Local storage APIs for Blazor applications.
- [HtmlAgilityPack](https://github.com/zzzprojects/html-agility-pack) - An agile HTML parser.

---

## License

This project is licensed under the terms of the [MIT](https://github.com/mantinband/amiko-dark/blob/main/LICENSE) license.