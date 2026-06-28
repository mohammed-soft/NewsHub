# Contributing Guidelines

Thank you for your interest in contributing to NewsHub. We appreciate your help in improving this project.

## Getting Started

### Reporting Issues

Before submitting an issue, please:

1. Search the existing [issues](../../issues) to avoid duplicates
2. Verify the issue is reproducible
3. Provide comprehensive information:
   - Operating system and version
   - .NET MAUI version
   - Steps to reproduce the issue
   - Expected behavior versus actual behavior
   - Screenshots or error logs when applicable

### Feature Requests

When proposing a new feature:

1. Clearly describe the intended functionality
2. Explain the value and use case
3. Suggest a potential implementation approach if possible
4. Be open to collaborative discussion

## Development Setup

### Setting Up Your Development Environment

1. Fork the repository on GitHub
2. Clone your fork locally:

```bash
git clone https://github.com/yourusername/NewsHub.git
cd NewsHub
```

3. Add the upstream repository:

```bash
git remote add upstream https://github.com/original-owner/NewsHub.git
```

4. Restore NuGet packages:

```bash
dotnet restore
```

5. Build the solution:

```bash
dotnet build
```

## Development Workflow

### Creating a Branch

Create a new branch for your work:

```bash
git checkout -b feature/your-feature-name
# or for bug fixes
git checkout -b fix/your-bug-fix
```

### Making Changes

- Follow the established code style and conventions
- Add clear comments for complex logic
- Ensure compatibility across all supported platforms
- Test your changes thoroughly

### Testing

Run tests on your target platforms:

```bash
# Windows
dotnet test -f net9.0-windows10.0.19041.0

# Android
dotnet test -f net9.0-android
```

### Committing Changes

Commit your changes with descriptive messages:

```bash
git add .
git commit -m "feat: add new feature description"
```

Follow [Conventional Commits](https://www.conventionalcommits.org/) specification:
- `feat:` for new features
- `fix:` for bug fixes
- `docs:` for documentation updates
- `style:` for formatting changes
- `refactor:` for code restructuring
- `test:` for adding or updating tests
- `chore:` for maintenance tasks

### Submitting Changes

1. Push your branch to your fork:

```bash
git push origin feature/your-feature-name
```

2. Create a Pull Request on GitHub
3. Provide a clear description of your changes
4. Reference any related issues

## Code Guidelines

### Code Style

- Use `PascalCase` for class names and method names
- Use `camelCase` for local variables and method parameters
- Use `_camelCase` for private fields
- Add XML documentation comments for public methods

### Code Example

```csharp
/// <summary>
/// Retrieves the latest news articles from the specified source
/// </summary>
/// <param name="source">The news source identifier</param>
/// <param name="cancellationToken">Cancellation token for async operation</param>
/// <returns>A list of news articles</returns>
public async Task<List<Article>> GetLatestNewsAsync(string source, CancellationToken cancellationToken = default)
{
    try
    {
        var articles = await _newsService.GetArticlesAsync(source, cancellationToken);
        return articles;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error fetching news from {Source}", source);
        throw;
    }
}
```

## Testing Requirements

- Ensure all new code is tested
- Add unit tests for new features
- Test on multiple platforms when possible
- Ensure all existing tests pass

## Documentation Standards

- Update README.md when adding new features
- Add XML documentation comments for public methods
- Update CHANGELOG files for significant changes

## Code of Conduct

### Our Commitment

We are committed to providing a welcoming and inclusive environment for all contributors. We expect all participants to:

- Show respect and professionalism in all interactions
- Focus on what is best for the community
- Demonstrate empathy and understanding toward other community members

### Unacceptable Behavior

The following behaviors are not permitted:

- Use of offensive language or inappropriate content
- Personal or political harassment
- Publishing private or sensitive information
- Any other unprofessional conduct

## Support

If you have questions or need assistance:

- Open an issue on GitHub for discussion
- Contact the maintainers at your.email@example.com

## Pull Request Checklist

Before submitting a Pull Request, verify the following:

- [ ] Code adheres to project style guidelines
- [ ] Tests have been added or updated as needed
- [ ] Documentation has been updated
- [ ] All tests pass successfully
- [ ] Solution builds without errors
- [ ] Changes have been tested on multiple platforms when applicable
- [ ] Commit messages follow the Conventional Commits specification

## Acknowledgments

Thank you for your interest in contributing to NewsHub. Your contributions help make this project better for everyone.
