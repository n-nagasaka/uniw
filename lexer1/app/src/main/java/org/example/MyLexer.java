package org.example;
import lombok.val;

public class MyLexer {

    public GetTokenResult getToken(String string) {

        val token = new Token(TokenType.If, "if");
        val result = new GetTokenResult(token, 2);

        return result;
    }
    
    public GetTokenResult getToken(String string, int start) {

        val token = new Token(TokenType.OpenParen, "(");
        val result = new GetTokenResult(token, start + 2);

        return result;
    }
}
