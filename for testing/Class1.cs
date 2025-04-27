using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace for_testing
{
    //enum TokenType { STRUCT, ID, TYPE, OPENBRACE, CLOSEBRACE, SEM, EOF }

    //class Token1
    //{
    //    public TokenType Type;
    //    public string Lexeme;
    //    public Token1(TokenType type, string lexeme) => (Type, Lexeme) = (type, lexeme);
    //}

    //class ParseResult
    //{
    //    public int Errors;
    //    public int Position;
    //    public List<string> ErrorMessages = new List<string>();
    //    public bool Success;

    //    public ParseResult Clone() =>
    //        new ParseResult
    //        {
    //            Errors = this.Errors,
    //            Position = this.Position,
    //            ErrorMessages = new List<string>(this.ErrorMessages),
    //            Success = this.Success
    //        };
    //}

    //class Parser
    //{
    //    List<Token1> tokens;
    //    Dictionary<(string rule, int pos), int> memo = new Dictionary<(string rule, int pos), int>();

    //    public Parser(List<Token1> tokens) => this.tokens = tokens;

    //    Token1 Current(int pos) => pos < tokens.Count ? tokens[pos] : new Token1(TokenType.EOF, "");

    //    ParseResult Try(ParseResult state, Func<ParseResult, ParseResult> func)
    //    {
    //        if (memo.TryGetValue((func.Method.Name, state.Position), out int minErr) && minErr <= state.Errors)
    //            return new ParseResult { Errors = int.MaxValue }; // Пропускаем бесполезную ветку

    //        var result = func(state.Clone());
    //        memo[(func.Method.Name, state.Position)] = result.Errors;
    //        return result;
    //    }

    //    public ParseResult Parse()
    //    {
    //        return ParseStart(new ParseResult { Position = 0 });
    //    }

    //    ParseResult ParseStart(ParseResult state)
    //    {
    //        var res = Try(state, ParseStruct);
    //        if (res.Success && Current(res.Position).Type == TokenType.SEM)
    //        {
    //            res.Position++;
    //            return res;
    //        }

    //        // Попробовать добавить SEM
    //        var addSem = res.Clone();
    //        addSem.Errors++;
    //        addSem.ErrorMessages.Add("Отсутствует ';' в конце структуры");
    //        return addSem;
    //    }

    //    ParseResult ParseStruct(ParseResult state)
    //    {
    //        Token1 token = Current(state.Position);
    //        if (token.Type == TokenType.STRUCT)
    //        {
    //            state.Position++;
    //        }
    //        else
    //        {
    //            // 3 варианта: добавить STRUCT, удалить, заменить
    //            return BestOf(
    //                () => AddToken(state, TokenType.STRUCT, "Ожидался 'struct'"),
    //                () => DeleteToken(state, "Удален лишний токен, ожидался 'struct'"),
    //                () => ReplaceToken(state, TokenType.STRUCT, "Заменен токен на 'struct'")
    //            );
    //        }

    //        // После 'struct' должен идти ID
    //        return ParseStructName(state);
    //    }

    //    ParseResult ParseStructName(ParseResult state)
    //    {
    //        Token1 token = Current(state.Position);
    //        if (token.Type == TokenType.ID)
    //        {
    //            state.Position++;
    //            return ParseFieldsBlock(state);
    //        }

    //        return BestOf(
    //            () => AddToken(state, TokenType.ID, "Отсутствует идентификатор структуры"),
    //            () => DeleteToken(state, "Удален неожиданный токен, ожидался ID"),
    //            () => ReplaceToken(state, TokenType.ID, "Заменен токен на ID")
    //        );
    //    }

    //    ParseResult ParseFieldsBlock(ParseResult state)
    //    {
    //        Token1 token = Current(state.Position);
    //        if (token.Type == TokenType.OPENBRACE)
    //        {
    //            state.Position++;
    //            return ParseField(state);
    //        }

    //        return BestOf(
    //            () => AddToken(state, TokenType.OPENBRACE, "Ожидалась '{' после ID"),
    //            () => DeleteToken(state, "Удален неожиданный токен, ожидалась '{'"),
    //            () => ReplaceToken(state, TokenType.OPENBRACE, "Заменен токен на '{'")
    //        );
    //    }

    //    ParseResult ParseField(ParseResult state)
    //    {
    //        Token1 token = Current(state.Position);

    //        if (token.Type == TokenType.CLOSEBRACE)
    //        {
    //            state.Position++;
    //            return new ParseResult { Position = state.Position, Errors = state.Errors, ErrorMessages = state.ErrorMessages, Success = true };
    //        }

    //        if (token.Type == TokenType.TYPE)
    //        {
    //            state.Position++;
    //            return ParseFieldName(state);
    //        }

    //        return BestOf(
    //            () => AddToken(state, TokenType.CLOSEBRACE, "Отсутствует '}'"),
    //            () => DeleteToken(state, "Удален неожиданный токен в теле структуры"),
    //            () => ReplaceToken(state, TokenType.TYPE, "Заменен токен на тип данных")
    //        );
    //    }

    //    ParseResult ParseFieldName(ParseResult state)
    //    {
    //        if (Current(state.Position).Type == TokenType.ID)
    //        {
    //            state.Position++;
    //            if (Current(state.Position).Type == TokenType.SEM)
    //            {
    //                state.Position++;
    //                return ParseField(state); // Рекурсивный вызов — следующее поле или '}'
    //            }
    //            else
    //            {
    //                return AddToken(state, TokenType.SEM, "Ожидалась ';' после имени поля");
    //            }
    //        }

    //        return BestOf(
    //            () => AddToken(state, TokenType.ID, "Ожидалось имя переменной"),
    //            () => DeleteToken(state, "Удален неожиданный токен, ожидался идентификатор"),
    //            () => ReplaceToken(state, TokenType.ID, "Заменен токен на имя переменной")
    //        );
    //    }

    //    ParseResult AddToken(ParseResult state, TokenType expected, string msg)
    //    {
    //        var newState = state.Clone();
    //        newState.Errors++;
    //        newState.ErrorMessages.Add(msg + $" (добавлен {expected})");
    //        return newState;
    //    }

    //    ParseResult DeleteToken(ParseResult state, string msg)
    //    {
    //        var newState = state.Clone();
    //        newState.Errors++;
    //        newState.ErrorMessages.Add(msg + $" (удален '{Current(state.Position).Lexeme}')");
    //        newState.Position++;
    //        return newState;
    //    }

    //    ParseResult ReplaceToken(ParseResult state, TokenType expected, string msg)
    //    {
    //        var newState = state.Clone();
    //        newState.Errors++;
    //        newState.ErrorMessages.Add(msg + $" (заменен на {expected})");
    //        newState.Position++;
    //        return newState;
    //    }

    //    ParseResult BestOf(params Func<ParseResult>[] options)
    //    {
    //        return options.Select(f => f()).OrderBy(r => r.Errors).First();
    //    }
    //}

}
