using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxGridView;

public partial class _Default : System.Web.UI.Page {

    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        ASPxGridView grid = sender as ASPxGridView;
        ModifyFilterExpression(ASPxComboBox1.Value.ToString(), ASPxTextBox1.Value, grid);
    }
    protected void ModifyFilterExpression(string FieldName, object value, ASPxGridView victim) {
        var criterias = CriteriaColumnAffinityResolver.SplitByColumns(CriteriaOperator.Parse(victim.FilterExpression));
                
        BinaryOperatorType operatorType;
        if (FieldName == "ProductName") {
            operatorType = BinaryOperatorType.Like;
            value += "%";
        } else
            operatorType = BinaryOperatorType.Equal;


        if (!criterias.Keys.Contains(new OperandProperty(FieldName)))
            criterias.Add(new OperandProperty(FieldName), new BinaryOperator(FieldName, value, operatorType));
        else
            criterias[new OperandProperty(FieldName)] = new BinaryOperator(FieldName, value, operatorType);
        victim.FilterExpression = CriteriaOperator.ToString(GroupOperator.And(criterias.Values));
    }

}