package org.example;
import java.util.List;
import java.util.Optional;
import java.util.regex.Pattern;
import java.util.stream.Stream;

import lombok.NonNull;

// import com.google.common.base.Optional;

import lombok.val;

public class MyLexer {

    public GetTokenResult getToken(String string) {

        val token = new Token(TokenType.If, "if");
        val result = new GetTokenResult(token, 2);

        return result;
    }
    
    public Optional<GetTokenResult> getToken(String string, int start) {
        val next = skipWhiteSpace(string, start);
        return matchTokens(string, next);
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

    public List<Token> tokenize(String s) {
        Stream.Builder<Token> sb = Stream.builder();
        var start = 0;
        while (true) {
            val res = getToken(s, start);
            if (res.isEmpty()) {
                return sb.build().toList();
            }
            val v = res.get();
            sb.accept(v.token());
            start = v.next();
        }
    }
}
