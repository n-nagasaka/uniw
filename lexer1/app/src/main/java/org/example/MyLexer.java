package org.example;
import java.util.Optional;
import java.util.regex.Pattern;

import lombok.NonNull;

// import com.google.common.base.Optional;

import lombok.val;

public class MyLexer {

    public GetTokenResult getToken(String string) {

        val token = new Token(TokenType.If, "if");
        val result = new GetTokenResult(token, 2);

        return result;
    }
    
    public GetTokenResult getToken(String string, int start) {
        val next = skipWhiteSpace(string, start);
        val result = matchTokens(string, next);
        if (result.isPresent()) {
            return result.get();
        }
        return new GetTokenResult(new Token(TokenType.Unknown, ""), next);
    }


    private int skipWhiteSpace(String s, int start) {
        int i = start;
        while (i < s.length() && Character.isWhitespace(s.charAt(i))) {
            i++;
        }
        return i;
    }

    private Optional<@NonNull GetTokenResult> matchTokens(String s, int start) {
        val p = Pattern.compile("^(?:if|\\(|[a-zA-Z_][a-zA-Z0-9_]*)");
        val m = p.matcher(s);
        m.region(start, s.length());
        if (m.find()) {
            val value = m.group();
            val tokenType = switch (value) {
                case "if" -> TokenType.If;
                case "(" -> TokenType.OpenParen;
                default -> TokenType.Symbol;
            };
            return Optional.of(new GetTokenResult(new Token(tokenType, value), start + value.length()));
        }
        return Optional.empty();
    }

}
