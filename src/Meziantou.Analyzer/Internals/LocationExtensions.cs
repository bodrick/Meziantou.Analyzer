﻿using Microsoft.CodeAnalysis;

namespace Meziantou.Analyzer.Internals;

internal static class LocationExtensions
{
    /// <summary>
    /// Gets the location in terms of path, line and column for a given token.
    /// </summary>
    /// <param name="token">The token to use.</param>
    /// <returns>The location in terms of path, line and column for a given token.</returns>
    internal static FileLinePositionSpan? GetLineSpan(this SyntaxToken token)
    {
        return token.SyntaxTree?.GetLineSpan(token.Span);
    }

    /// <summary>
    /// Gets the location in terms of path, line and column for a given node.
    /// </summary>
    /// <param name="node">The node to use.</param>
    /// <returns>The location in terms of path, line and column for a given node.</returns>
    internal static FileLinePositionSpan? GetLineSpan(this SyntaxNode node)
    {
        return node.SyntaxTree?.GetLineSpan(node.Span);
    }

    /// <summary>
    /// Gets the location in terms of path, line and column for a given trivia.
    /// </summary>
    /// <param name="trivia">The trivia to use.</param>
    /// <returns>The location in terms of path, line and column for a given trivia.</returns>
    internal static FileLinePositionSpan? GetLineSpan(this SyntaxTrivia trivia)
    {
        return trivia.SyntaxTree?.GetLineSpan(trivia.Span);
    }

    /// <summary>
    /// Gets the location in terms of path, line and column for a given node or token.
    /// </summary>
    /// <param name="nodeOrToken">The trivia to use.</param>
    /// <returns>The location in terms of path, line and column for a given node or token.</returns>
    internal static FileLinePositionSpan? GetLineSpan(this SyntaxNodeOrToken nodeOrToken)
    {
        return nodeOrToken.SyntaxTree?.GetLineSpan(nodeOrToken.Span);
    }

    /// <summary>
    /// Gets the line on which the given token occurs.
    /// </summary>
    /// <param name="token">The token to use.</param>
    /// <returns>The line on which the given token occurs.</returns>
    internal static int? GetLine(this SyntaxToken token)
    {
        return token.GetLineSpan()?.StartLinePosition.Line;
    }

    /// <summary>
    /// Gets the line on which the given node occurs.
    /// </summary>
    /// <param name="node">The node to use.</param>
    /// <returns>The line on which the given node occurs.</returns>
    internal static int? GetLine(this SyntaxNode node)
    {
        return node.GetLineSpan()?.StartLinePosition.Line;
    }

    /// <summary>
    /// Gets the line on which the given trivia occurs.
    /// </summary>
    /// <param name="trivia">The trivia to use.</param>
    /// <returns>The line on which the given trivia occurs.</returns>
    internal static int? GetLine(this SyntaxTrivia trivia)
    {
        return trivia.GetLineSpan()?.StartLinePosition.Line;
    }

    /// <summary>
    /// Gets the end line of the given token.
    /// </summary>
    /// <param name="token">The token to use.</param>
    /// <returns>The line on which the given token ends.</returns>
    internal static int? GetEndLine(this SyntaxToken token)
    {
        return token.GetLineSpan()?.EndLinePosition.Line;
    }

    /// <summary>
    /// Gets the end line of the given node.
    /// </summary>
    /// <param name="node">The node to use.</param>
    /// <returns>The line on which the given node ends.</returns>
    internal static int? GetEndLine(this SyntaxNode node)
    {
        return node.GetLineSpan()?.EndLinePosition.Line;
    }

    /// <summary>
    /// Gets the end line of the given trivia.
    /// </summary>
    /// <param name="trivia">The trivia to use.</param>
    /// <returns>The line on which the given trivia ends.</returns>
    internal static int? GetEndLine(this SyntaxTrivia trivia)
    {
        return trivia.GetLineSpan()?.EndLinePosition.Line;
    }

    /// <summary>
    /// Get a value indicating whether the given node span multiple source text lines.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>True, if the node spans multiple source text lines.</returns>
    internal static bool SpansMultipleLines(this SyntaxNode node)
    {
        var lineSpan = node.GetLineSpan();
        if (lineSpan == null)
            return false;

        return lineSpan.Value.StartLinePosition.Line < lineSpan.Value.EndLinePosition.Line;
    }

    /// <summary>
    /// Gets a value indicating whether the given trivia span multiple source text lines.
    /// </summary>
    /// <param name="trivia">The trivia to check.</param>
    /// <returns>
    /// <see langword="true"/> if the trivia spans multiple source text lines; otherwise, <see langword="false"/>.
    /// </returns>
    internal static bool SpansMultipleLines(this SyntaxTrivia trivia)
    {
        var lineSpan = trivia.GetLineSpan();
        if (lineSpan == null)
            return false;

        return lineSpan.Value.StartLinePosition.Line < lineSpan.Value.EndLinePosition.Line;
    }
}
