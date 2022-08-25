namespace Tuyin.IR.Compiler.Target.Visitors
{
    abstract class AstVisitor
    {
        public void Visit(IAstNode ast)
        {
            switch (ast.AstType)
            {
                case AstNodeType.Type: VisitType(ast as SourceType); return;
                case AstNodeType.ExprAdd: VisitExprAdd(ast as ExprAdd); return;
                case AstNodeType.ExprAnd: VisitExprAnd(ast as ExprAnd); return;
                case AstNodeType.ExprAndAnd: VisitExprAndAnd(ast as ExprAndAnd); return;
                case AstNodeType.ExprArray: VisitExprArray(ast as ExprArray); return;
                case AstNodeType.ExprCall: VisitExprCall(ast as ExprCall); return;
                case AstNodeType.ExprConditional: VisitExprConditional(ast as ExprConditional); return;
                case AstNodeType.ExprDiv: VisitExprDiv(ast as ExprDiv); return;
                case AstNodeType.ExprEqual: VisitExprEqual(ast as ExprEqual); return;
                case AstNodeType.ExprFalse: VisitExprFalse(ast as ExprFalse); return;
                case AstNodeType.ExprFloat: VisitExprFloat(ast as ExprFloat); return;
                case AstNodeType.ExprGreaterEqual: VisitExprGreaterEqual(ast as ExprGreaterEqual); return;
                case AstNodeType.ExprGreaterThen: VisitExprGreaterThen(ast as ExprGreaterThen); return;
                case AstNodeType.ExprIdentifier: VisitExprIdentifier(ast as ExprIdentifier); return;
                case AstNodeType.ExprIndex: VisitExprIndex(ast as ExprIndex); return;
                case AstNodeType.ExprInteger: VisitExprInteger(ast as ExprInteger); return;
                case AstNodeType.ExprLeftShift: VisitExprLeftShift(ast as ExprLeftShift); return;
                case AstNodeType.ExprLessEqual: VisitExprLessEqual(ast as ExprLessEqual); return;
                case AstNodeType.ExprLessThen: VisitExprLessThen(ast as ExprLessThen); return;
                case AstNodeType.ExprLiteral: VisitExprLiteral(ast as ExprLiteral); return;
                case AstNodeType.ExprMul: VisitExprMul(ast as ExprMul); return;
                case AstNodeType.ExprNeg: VisitExprNeg(ast as ExprNeg); return;
                case AstNodeType.ExprNotEqual: VisitExprNotEqual(ast as ExprNotEqual); return;
                case AstNodeType.ExprOr: VisitExprOr(ast as ExprOr); return;
                case AstNodeType.ExprOrOr: VisitExprOrOr(ast as ExprOrOr); return;
                case AstNodeType.ExprPlus: VisitExprPlus(ast as ExprPlus); return;
                case AstNodeType.ExprPostDec: VisitExprPostDec(ast as ExprPostDec); return;
                case AstNodeType.ExprPostInc: VisitExprPostInc(ast as ExprPostInc); return;
                case AstNodeType.ExprPreDec: VisitExprPreDec(ast as ExprPreDec); return;
                case AstNodeType.ExprPreInc: VisitExprPreInc(ast as ExprPreInc); return;
                case AstNodeType.ExprRem: VisitExprRem(ast as ExprRem); return;
                case AstNodeType.ExprRightShift: VisitExprRightShift(ast as ExprRightShift); return;
                case AstNodeType.ExprString: VisitExprString(ast as ExprString); return;
                case AstNodeType.ExprSub: VisitExprSub(ast as ExprSub); return;
                case AstNodeType.ExprTrue: VisitExprTrue(ast as ExprTrue); return;
                case AstNodeType.ExprUnicode: VisitExprUnicode(ast as ExprUnicode); return;
                case AstNodeType.ExprXor: VisitExprXor(ast as ExprXor); return;
                case AstNodeType.Import: VisitImport(ast as Import); return;
                case AstNodeType.StmtBlock: VisitStmtBlock(ast as StmtBlock); return;
                case AstNodeType.StmtBreak: VisitStmtBreak(ast as StmtBreak); return;
                case AstNodeType.StmtContinue: VisitStmtContinue(ast as StmtContinue); return;
                case AstNodeType.StmtExpression: VisitStmtExpression(ast as StmtExpression); return;
                case AstNodeType.StmtIf: VisitStmtIf(ast as StmtIf); return;
                case AstNodeType.StmtReturn: VisitStmtReturn(ast as StmtReturn); return;
                case AstNodeType.StmtSwitch: VisitStmtSwitch(ast as StmtSwitch); return;
                case AstNodeType.StmtVarDecl: VisitStmtVarDecl(ast as StmtVarDecl); return;
                case AstNodeType.StmtWhile: VisitStmtWhile(ast as StmtWhile); return;
                case AstNodeType.Production: VisitProduction(ast as SourceProduction); return;
                case AstNodeType.Regex: VisitRegex(ast as SourceExpression); return;
            }

            throw new NotImplementedException();
        }

        public virtual void VisitTarget(Target target)
        {
            foreach (var import in target.Imports)
                VisitImport(import);

            foreach (var decl in target.Declares)
                VisitDeclare(decl);
        }

        public virtual void VisitDeclare(Declare decl) 
        {
            switch (decl.DeclareType)
            {
                case DeclareType.Match:
                    VisitMatch(decl as MatchDecl);
                    break;
                case DeclareType.Struct:
                    VisitStrcut(decl as StructDecl);
                    break;
                case DeclareType.Function:
                    VisitFunction(decl as FuncDecl);
                    break;
            }
        }

        public virtual void VisitStrcut(StructDecl @struct) 
        {

        }

        public virtual void VisitFunction(FuncDecl func)
        {
            Visit(func.Body);
        }

        public virtual void VisitMatch(MatchDecl frag)
        {
            Visit(frag.Production);
        }

        protected abstract void VisitRegex(SourceExpression sourceExpression);
        protected abstract void VisitProduction(SourceProduction sourceProduction);
        protected abstract void VisitStmtWhile(StmtWhile stmtWhile);
        protected abstract void VisitStmtVarDecl(StmtVarDecl stmtVarDecl);
        protected abstract void VisitStmtSwitch(StmtSwitch stmtSwitch);
        protected abstract void VisitStmtReturn(StmtReturn stmtReturn);
        protected abstract void VisitStmtIf(StmtIf stmtIf);
        protected abstract void VisitStmtExpression(StmtExpression stmtExpression);
        protected abstract void VisitStmtContinue(StmtContinue stmtContinue);
        protected abstract void VisitStmtBreak(StmtBreak stmtBreak);
        protected abstract void VisitStmtBlock(StmtBlock stmtBlock);
        protected abstract void VisitImport(Import import);
        protected abstract void VisitExprXor(ExprXor exprXor);
        protected abstract void VisitExprUnicode(ExprUnicode exprUnicode);
        protected abstract void VisitExprTrue(ExprTrue exprTrue);
        protected abstract void VisitExprSub(ExprSub exprSub);
        protected abstract void VisitExprString(ExprString exprString);
        protected abstract void VisitExprRightShift(ExprRightShift exprRightShift);
        protected abstract void VisitExprRem(ExprRem exprRem);
        protected abstract void VisitExprPreInc(ExprPreInc exprPreInc);
        protected abstract void VisitExprPreDec(ExprPreDec exprPreDec);
        protected abstract void VisitExprPostInc(ExprPostInc exprPostInc);
        protected abstract void VisitExprPostDec(ExprPostDec exprPostDec);
        protected abstract void VisitExprPlus(ExprPlus exprPlus);
        protected abstract void VisitExprOrOr(ExprOrOr exprOrOr);
        protected abstract void VisitExprOr(ExprOr exprOr);
        protected abstract void VisitExprNotEqual(ExprNotEqual exprNotEqual);
        protected abstract void VisitExprNeg(ExprNeg exprNeg);
        protected abstract void VisitExprMul(ExprMul exprMul);
        protected abstract void VisitExprLiteral(ExprLiteral exprLiteral);
        protected abstract void VisitExprLessThen(ExprLessThen exprLessThen);
        protected abstract void VisitExprLessEqual(ExprLessEqual exprLessEqual);
        protected abstract void VisitExprLeftShift(ExprLeftShift exprLeftShift);
        protected abstract void VisitExprInteger(ExprInteger exprInteger);
        protected abstract void VisitExprIndex(ExprIndex exprIndex);
        protected abstract void VisitExprIdentifier(ExprIdentifier exprIdentifier);
        protected abstract void VisitExprGreaterThen(ExprGreaterThen exprGreaterThen);
        protected abstract void VisitExprGreaterEqual(ExprGreaterEqual exprGreaterEqual);
        protected abstract void VisitExprFloat(ExprFloat exprFloat);
        protected abstract void VisitExprFalse(ExprFalse exprFalse);
        protected abstract void VisitExprEqual(ExprEqual exprEqual);
        protected abstract void VisitExprDiv(ExprDiv exprDiv);
        protected abstract void VisitExprConditional(ExprConditional exprConditional);
        protected abstract void VisitExprCall(ExprCall exprCall);
        protected abstract void VisitExprArray(ExprArray exprArray);
        protected abstract void VisitExprAndAnd(ExprAndAnd exprAndAnd);
        protected abstract void VisitExprAnd(ExprAnd exprAnd);
        protected abstract void VisitExprAdd(ExprAdd exprAdd);
        protected abstract void VisitType(SourceType sourceType);
    }

    abstract class AstVisitor<T>
    {
        public T Visit(IAstNode ast)
        {
            switch (ast.AstType)
            {
                case AstNodeType.Type: return VisitType(ast as SourceType);
                case AstNodeType.ExprAdd: return VisitExprAdd(ast as ExprAdd);
                case AstNodeType.ExprAnd: return VisitExprAnd(ast as ExprAnd);
                case AstNodeType.ExprAndAnd: return VisitExprAndAnd(ast as ExprAndAnd);
                case AstNodeType.ExprArray: return VisitExprArray(ast as ExprArray);
                case AstNodeType.ExprCall: return VisitExprCall(ast as ExprCall);
                case AstNodeType.ExprConditional: return VisitExprConditional(ast as ExprConditional);
                case AstNodeType.ExprDiv: return VisitExprDiv(ast as ExprDiv);
                case AstNodeType.ExprEqual: return VisitExprEqual(ast as ExprEqual);
                case AstNodeType.ExprFalse: return VisitExprFalse(ast as ExprFalse);
                case AstNodeType.ExprFloat: return VisitExprFloat(ast as ExprFloat);
                case AstNodeType.ExprGreaterEqual: return VisitExprGreaterEqual(ast as ExprGreaterEqual);
                case AstNodeType.ExprGreaterThen: return VisitExprGreaterThen(ast as ExprGreaterThen);
                case AstNodeType.ExprIdentifier: return VisitExprIdentifier(ast as ExprIdentifier);
                case AstNodeType.ExprIndex: return VisitExprIndex(ast as ExprIndex);
                case AstNodeType.ExprInteger: return VisitExprInteger(ast as ExprInteger);
                case AstNodeType.ExprLeftShift: return VisitExprLeftShift(ast as ExprLeftShift);
                case AstNodeType.ExprLessEqual: return VisitExprLessEqual(ast as ExprLessEqual);
                case AstNodeType.ExprLessThen: return VisitExprLessThen(ast as ExprLessThen);
                case AstNodeType.ExprLiteral: return VisitExprLiteral(ast as ExprLiteral);
                case AstNodeType.ExprMul: return VisitExprMul(ast as ExprMul);
                case AstNodeType.ExprNeg: return VisitExprNeg(ast as ExprNeg);
                case AstNodeType.ExprNotEqual: return VisitExprNotEqual(ast as ExprNotEqual);
                case AstNodeType.ExprOr: return VisitExprOr(ast as ExprOr);
                case AstNodeType.ExprOrOr: return VisitExprOrOr(ast as ExprOrOr);
                case AstNodeType.ExprPlus: return VisitExprPlus(ast as ExprPlus);
                case AstNodeType.ExprPostDec: return VisitExprPostDec(ast as ExprPostDec);
                case AstNodeType.ExprPostInc: return VisitExprPostInc(ast as ExprPostInc);
                case AstNodeType.ExprPreDec: return VisitExprPreDec(ast as ExprPreDec);
                case AstNodeType.ExprPreInc: return VisitExprPreInc(ast as ExprPreInc);
                case AstNodeType.ExprRem: return VisitExprRem(ast as ExprRem);
                case AstNodeType.ExprRightShift: return VisitExprRightShift(ast as ExprRightShift);
                case AstNodeType.ExprString: return VisitExprString(ast as ExprString);
                case AstNodeType.ExprSub: return VisitExprSub(ast as ExprSub);
                case AstNodeType.ExprTrue: return VisitExprTrue(ast as ExprTrue);
                case AstNodeType.ExprUnicode: return VisitExprUnicode(ast as ExprUnicode);
                case AstNodeType.ExprXor: return VisitExprXor(ast as ExprXor);
                case AstNodeType.Import: return VisitImport(ast as Import);
                case AstNodeType.StmtBlock: return VisitStmtBlock(ast as StmtBlock);
                case AstNodeType.StmtBreak: return VisitStmtBreak(ast as StmtBreak);
                case AstNodeType.StmtContinue: return VisitStmtContinue(ast as StmtContinue);
                case AstNodeType.StmtExpression: return VisitStmtExpression(ast as StmtExpression);
                case AstNodeType.StmtIf: return VisitStmtIf(ast as StmtIf);
                case AstNodeType.StmtReturn: return VisitStmtReturn(ast as StmtReturn);
                case AstNodeType.StmtSwitch: return VisitStmtSwitch(ast as StmtSwitch);
                case AstNodeType.StmtVarDecl: return VisitStmtVarDecl(ast as StmtVarDecl);
                case AstNodeType.StmtWhile: return VisitStmtWhile(ast as StmtWhile);
                case AstNodeType.Production: return VisitProduction(ast as SourceProduction);
                case AstNodeType.Regex: return VisitRegex(ast as SourceExpression);
            }

            throw new NotImplementedException();
        }

        public virtual void VisitTarget(Target target)
        {
            foreach (var import in target.Imports)
                VisitImport(import);

            foreach (var decl in target.Declares)
                VisitDeclare(decl);
        }

        public virtual void VisitDeclare(Declare decl)
        {
            switch (decl.DeclareType)
            {
                case DeclareType.Match:
                    VisitMatch(decl as MatchDecl);
                    break;
                case DeclareType.Struct:
                    VisitStrcut(decl as StructDecl);
                    break;
                case DeclareType.Function:
                    VisitFunction(decl as FuncDecl);
                    break;
            }
        }

        public virtual void VisitStrcut(StructDecl @struct)
        {

        }

        public virtual void VisitFunction(FuncDecl func)
        {
            Visit(func.Body);
        }

        public virtual void VisitMatch(MatchDecl frag)
        {
            Visit(frag.Production);
        }

        protected abstract T VisitRegex(SourceExpression sourceExpression);
        protected abstract T VisitProduction(SourceProduction sourceProduction);
        protected abstract T VisitStmtWhile(StmtWhile stmtWhile);
        protected abstract T VisitStmtVarDecl(StmtVarDecl stmtVarDecl);
        protected abstract T VisitStmtSwitch(StmtSwitch stmtSwitch);
        protected abstract T VisitStmtReturn(StmtReturn stmtReturn);
        protected abstract T VisitStmtIf(StmtIf stmtIf);
        protected abstract T VisitStmtExpression(StmtExpression stmtExpression);
        protected abstract T VisitStmtContinue(StmtContinue stmtContinue);
        protected abstract T VisitStmtBreak(StmtBreak stmtBreak);
        protected abstract T VisitStmtBlock(StmtBlock stmtBlock);
        protected abstract T VisitImport(Import import);
        protected abstract T VisitExprXor(ExprXor exprXor);
        protected abstract T VisitExprUnicode(ExprUnicode exprUnicode);
        protected abstract T VisitExprTrue(ExprTrue exprTrue);
        protected abstract T VisitExprSub(ExprSub exprSub);
        protected abstract T VisitExprString(ExprString exprString);
        protected abstract T VisitExprRightShift(ExprRightShift exprRightShift);
        protected abstract T VisitExprRem(ExprRem exprRem);
        protected abstract T VisitExprPreInc(ExprPreInc exprPreInc);
        protected abstract T VisitExprPreDec(ExprPreDec exprPreDec);
        protected abstract T VisitExprPostInc(ExprPostInc exprPostInc);
        protected abstract T VisitExprPostDec(ExprPostDec exprPostDec);
        protected abstract T VisitExprPlus(ExprPlus exprPlus);
        protected abstract T VisitExprOrOr(ExprOrOr exprOrOr);
        protected abstract T VisitExprOr(ExprOr exprOr);
        protected abstract T VisitExprNotEqual(ExprNotEqual exprNotEqual);
        protected abstract T VisitExprNeg(ExprNeg exprNeg);
        protected abstract T VisitExprMul(ExprMul exprMul);
        protected abstract T VisitExprLiteral(ExprLiteral exprLiteral);
        protected abstract T VisitExprLessThen(ExprLessThen exprLessThen);
        protected abstract T VisitExprLessEqual(ExprLessEqual exprLessEqual);
        protected abstract T VisitExprLeftShift(ExprLeftShift exprLeftShift);
        protected abstract T VisitExprInteger(ExprInteger exprInteger);
        protected abstract T VisitExprIndex(ExprIndex exprIndex);
        protected abstract T VisitExprIdentifier(ExprIdentifier exprIdentifier);
        protected abstract T VisitExprGreaterThen(ExprGreaterThen exprGreaterThen);
        protected abstract T VisitExprGreaterEqual(ExprGreaterEqual exprGreaterEqual);
        protected abstract T VisitExprFloat(ExprFloat exprFloat);
        protected abstract T VisitExprFalse(ExprFalse exprFalse);
        protected abstract T VisitExprEqual(ExprEqual exprEqual);
        protected abstract T VisitExprDiv(ExprDiv exprDiv);
        protected abstract T VisitExprConditional(ExprConditional exprConditional);
        protected abstract T VisitExprCall(ExprCall exprCall);
        protected abstract T VisitExprArray(ExprArray exprArray);
        protected abstract T VisitExprAndAnd(ExprAndAnd exprAndAnd);
        protected abstract T VisitExprAnd(ExprAnd exprAnd);
        protected abstract T VisitExprAdd(ExprAdd exprAdd);
        protected abstract T VisitType(SourceType sourceType);
    }
}
