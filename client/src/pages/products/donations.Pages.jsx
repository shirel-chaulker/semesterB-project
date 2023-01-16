import React, { useState } from "react";
import { AddProductToDB } from "../../services/userServices";

export const DonationsPage = (props) => {
  const [addProduct, SetAddProduct] = useState({
    ProductName: "",
    Product_Value: "",
    donate_company: "",
    NonProfitName: "",
    CampaignName: "",
  });

  const HandleUserInput = async () => {
    let json = addProduct;
    json.Product_Value = parseInt(addProduct.Product_Value);
    await AddProductToDB(json);
    SetAddProduct({});
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  return (
    <div className="student-inputs ">
      <div className="input-group mb-3">
        <span className="input-group-text"> ProductName</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            SetAddProduct({ ...addProduct, ProductName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">Product_Value</span>
        <input
          className="form-control"
          type="number"
          onChange={(o) => {
            SetAddProduct({ ...addProduct, Product_Value: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">donate_company</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            SetAddProduct({ ...addProduct, donate_company: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">NonProfitName</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            SetAddProduct({ ...addProduct, NonProfitName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">CampaignName</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            SetAddProduct({ ...addProduct, CampaignName: o.target.value });
          }}
        />
      </div>
      <button className="btn btn-success" onClick={HandleUserInput}>
        Submit
      </button>
    </div>
  );
};
