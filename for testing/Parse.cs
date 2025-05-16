using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace for_testing
{
    public enum ERRORTYPE { DELETE, ADD, CHANGE,ADDEND, NONE,START}
    public class Parse
    {
        public Token GetNeedToken(List<Token> tokencurrent)
        {
            if (tokencurrent.Count != 0)
            {
                if (tokencurrent[tokencurrent.Count - 1] == Token.IF)
                {
                    return Token.ID;
                }
                else if (tokencurrent[tokencurrent.Count - 1] == Token.ID)
                {
                    if (tokencurrent[tokencurrent.Count - 2] == Token.OPERATOR || tokencurrent[tokencurrent.Count - 2] == Token.EQUELS)
                    {
                        return Token.THEN;
                    }
                    else if (tokencurrent[tokencurrent.Count - 2] == Token.IF)
                    {
                        return Token.OPERATOR;
                    }
                    else if (tokencurrent[tokencurrent.Count - 3] == Token.THEN)
                    {
                        return Token.ELSE;
                    }
                    else if (tokencurrent[tokencurrent.Count - 3] == Token.ELSE)
                    {
                        return Token.SEMICOLON;
                    }

                }
                else if (tokencurrent[tokencurrent.Count - 1] == Token.GOTO)
                {
                    return Token.ID;
                }
                else if (tokencurrent[tokencurrent.Count - 1] == Token.OPERATOR)
                {
                    return Token.ID;
                }
                else if (tokencurrent[tokencurrent.Count - 1] == Token.EQUELS)
                {
                    if (tokencurrent[tokencurrent.Count - 2] == Token.ID || tokencurrent[tokencurrent.Count - 2] == Token.NUMBER)
                    {
                        return Token.EQUELS;

                    }
                    else
                    {
                        return Token.ID;
                    }
                }
            
                else if (tokencurrent[tokencurrent.Count - 1] == Token.THEN || tokencurrent[tokencurrent.Count - 1] == Token.ELSE)
                {
                    return Token.GOTO;
                }
                else if (tokencurrent[tokencurrent.Count - 1] == Token.NUMBER)
                {
                    if (tokencurrent[tokencurrent.Count - 2] == Token.OPERATOR || tokencurrent[tokencurrent.Count - 2] == Token.EQUELS)
                    {
                        return Token.THEN;
                    }
                    else if (tokencurrent[tokencurrent.Count - 2] == Token.IF)
                    {
                        return Token.OPERATOR;
                    }
                    else if (tokencurrent[tokencurrent.Count - 3] == Token.THEN)
                    {
                        return Token.ELSE;
                    }
                    else if (tokencurrent[tokencurrent.Count - 3] == Token.ELSE)
                    {
                        return Token.SEMICOLON;
                    }
                }
                else if (tokencurrent[tokencurrent.Count - 1] == Token.SEMICOLON)
                {
                    return Token.IF;
                }
                else if (tokencurrent[tokencurrent.Count - 1] == Token.VOSKL)
                {
                    return Token.EQUELS;
                }
                
            }
            else
            {
                return Token.IF;
            }
            return Token.UNKNOWN;
        }
        public List<string> Errors = new List<string>();    
        public List<Token> tokens = new List<Token>();
        public List<Token> current_tokens = new List<Token>();
        public int Current_Position = 0;
        public List<string> mes = new List<string>();
        public Parse(List<Token> tok,List<string> fortoken) {
            tokens = tok;
            mes = fortoken;
        }
        int absolute = 0;
        public List<MyToken> myTokens = new List<MyToken>();
        public int Parser(ERRORTYPE error, int current_token, ref TokenReturn token, int errorCount = 0)
        {
            if (myTokens.Count != 0)
            {
                Token how = GetNeedToken(token.tokens);
                if (error == ERRORTYPE.DELETE)
                {
                    token.error.Add(new MyToken($"Ожидается   \"{how}\"", myTokens[current_token].Position, myTokens[current_token].Line));
                    current_token++;
                    errorCount++;
                }
                else if (error == ERRORTYPE.CHANGE)
                {
                    token.error.Add(new MyToken($"Ожидается \"{how}\",было  получено \"{myTokens[current_token].Value}\"", myTokens[current_token].Position, myTokens[current_token].Line));
                    token.tokens.Add(GetNeedToken(token.tokens));
                    current_token++;
                    errorCount++;
                }
                else if (error == ERRORTYPE.ADD)
                {
                    token.error.Add(new MyToken($"Ожидается \"{how}\"", myTokens[current_token].Position, myTokens[current_token].Line));
                    token.tokens.Add(GetNeedToken(token.tokens));
                    errorCount++;
                }
                else if (error == ERRORTYPE.ADDEND)
                {
                    token.error.Add(new MyToken($"Ожидается \"{how}\"", myTokens[myTokens.Count - 1].Position + 3, myTokens[myTokens.Count - 1].Line));
                    token.tokens.Add(GetNeedToken(token.tokens));
                    errorCount++;
                }
                else if (error == ERRORTYPE.NONE)
                {
                    token.tokens.Add(tokens[current_token]);
                    current_token++;
                }
                Token what = GetNeedToken(token.tokens);
                if (tokens.Count > current_token)
                {
                    if (what != tokens[current_token])
                    {
                        if ((what == Token.ID && tokens[current_token] == Token.NUMBER) ||
                            (what == Token.ID && tokens[current_token] == Token.EQUELS &&
                            ((token.tokens[token.tokens.Count - 1] == Token.OPERATOR || token.tokens[token.tokens.Count- 1] == Token.EQUELS) &&
                            (token.tokens[token.tokens.Count - 2] == Token.ID || token.tokens[token.tokens.Count - 2] == Token.NUMBER))) ||
                            (what == Token.ELSE && tokens[current_token] == Token.SEMICOLON) ||
                            (what == Token.OPERATOR && tokens[current_token] == Token.EQUELS) ||
                            (what == Token.OPERATOR && tokens[current_token] == Token.VOSKL))
                        {
                            return Parser(ERRORTYPE.NONE, current_token, ref token, errorCount);
                        }
                        else
                        {
                            int minErrors = int.MaxValue;

                            var token1 = new TokenReturn(token.tokens, token.error);

                            int errorsDelete = Parser(ERRORTYPE.DELETE, current_token, ref token1, errorCount);
                            if (errorsDelete < minErrors)
                                minErrors = errorsDelete;
                            var token2 = new TokenReturn(token.tokens, token.error);
                            int errorsChange = Parser(ERRORTYPE.CHANGE, current_token, ref token2, errorCount);
                            if (errorsChange < minErrors)
                            {
                                minErrors = errorsChange;
                                token1 = new TokenReturn(token2.tokens, token2.error);
                            }
                            var token3 = new TokenReturn(token.tokens, token.error);
                            int errorsAdd = Parser(ERRORTYPE.ADD, current_token, ref token3, errorCount);
                            if (errorsAdd < minErrors)
                            {
                                minErrors = errorsAdd;
                                token1 = new TokenReturn(token3.tokens, token3.error);
                            }
                            token = token1;
                            return minErrors;
                        }
                    }
                    else
                    {
                        return Parser(ERRORTYPE.NONE, current_token, ref token, errorCount);
                    }
                }
                else
                {
                    int if1=0;
                    int b = 0;
                    for (int i = 0; i < token.tokens.Count; i++) {
                        if (token.tokens[i] == Token.IF )
                        {
                            if1++;
                            b = 1;
                        }
                        if(token.tokens[i] == Token.SEMICOLON && if1 != 0)
                        {
                            if1--;
                        }
                    }
                    
                    if (if1 != 0 && token.tokens[token.tokens.Count - 1] != Token.SEMICOLON)
                    {
                        
                        var token3 = new TokenReturn(token.tokens, token.error);
                        int errorsAdd = Parser(ERRORTYPE.ADDEND, current_token, ref token3, errorCount);
                        token = token3;
                        return errorsAdd;
                    }

                }
                return errorCount;
            }

            return errorCount;
        }

    }
}
