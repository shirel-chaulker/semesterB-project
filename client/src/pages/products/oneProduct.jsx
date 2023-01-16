import React, { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { RingLoader } from "react-spinners";
import { getProductById } from "../../services/userServices";

export const OneProduct = (props) => {
  const [Product, SetProduct] = useState();
  const navigate = useNavigate();
  const location = useLocation();
  const { ProductID } = location.state;

  const initProductData = async () => {
    let product = await getProductById(ProductID);
    SetProduct(product);
  };

  useEffect(() => {
    initProductData();
  }, []);

  return Product && Product !== undefined ? (
    <div className="card campaign-container">
      <div className="card-title">
        {Product.ProductID && <h5>Product ID: {Product.ProductID}</h5>}
        {Product.ProductName && <h5>ProductName: {Product.ProductName}</h5>}
        {Product.Product_Value && (
          <h2 className="card-body">{Product.Product_Value}</h2>
        )}
        {Product.donate_company && (
          <h5>donate_company: {Product.donate_company}</h5>
        )}
        {Product.NonProfitName && (
          <h5>Campaign description: {Product.NonProfitName}</h5>
        )}
        {Product.CampaignName && <h5>CampaignName: {Product.CampaignName}</h5>}

        <Link to="/productsTbl">
          <button className="btn btn-primary">Return To Product table</button>
        </Link>
      </div>
    </div>
  ) : (
    <div className="spinner-app">
      <RingLoader color="#8d8de3" size={300} />
    </div>
  );
};
