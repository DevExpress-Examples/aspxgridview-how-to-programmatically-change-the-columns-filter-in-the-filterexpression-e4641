using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page {

    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        ASPxGridView grid = sender as ASPxGridView;
        ModifyFilterExpression(ASPxComboBox1.Value.ToString(), ASPxTextBox1.Value, grid);
    }
    protected void ModifyFilterExpression(string FieldName, object value, ASPxGridView targetGrid) {
        var criterias = CriteriaColumnAffinityResolver.SplitByColumnNames(CriteriaOperator.Parse(targetGrid.FilterExpression)).Item2;

        CriteriaOperator co = null;
        if (FieldName == "ProductName") {
            value += "%";
            co = new FunctionOperator("Like", new OperandProperty(FieldName), new OperandValue(value));
        } else
            co = new BinaryOperator(FieldName, value, BinaryOperatorType.Equal);
        if (!criterias.Keys.Contains(FieldName))
            criterias.Add(FieldName, co); 
        else
            criterias[FieldName] = co; 
        targetGrid.FilterExpression = CriteriaOperator.ToString(GroupOperator.And(criterias.Values));
    }

}