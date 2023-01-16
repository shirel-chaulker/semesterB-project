import React, { useContext, useEffect, useState } from "react";
import { RingLoader } from "react-spinners";
import { Link } from "react-router-dom";
import { Users } from "../users";
import { useNavigate } from "react-router-dom";
import { GetProducts } from "../../services/userServices";
import { ThemeContext } from "../../context/theme.context";
import { PurchasePage } from "../socialActivist/purchasePage";
import { DonationsPage } from "./donations.Pages";

export const ProductTbl = () => {
  const [ProductArr, SetProductArr] = useState();
  const navigate = useNavigate();
  let { roles } = useContext(ThemeContext);
  let GetProductsData = async () => {
    let ArrProduct = await GetProducts();
    let ArrPro = Object.values(ArrProduct);
    SetProductArr(ArrPro);
  };

  useEffect(() => {
    GetProductsData();
  });

  // let roles = Users();
  // if (roles !== null) console.log(roles, "its work");
  let GoToProduct = (ProductID) => {
    console.log(ProductID);

    navigate("/productInfo", {
      state: {
        ProductID,
      },
    });
  };

  return (
    <div>
      <table class="table table-dark table-hover">
        <thead>
          <tr>
            <th scope="col">ProductID</th>
            <th scope="col">ProductName</th>
            <th scope="col">Product_Value</th>
            <th scope="col">donate_company</th>
            <th scope="col">NonProfitName</th>
            <th scope="col">CampaignName</th>
            {/* <th scope="col"></th> */}
          </tr>
        </thead>
        {ProductArr && ProductArr !== undefined ? (
          ProductArr.map((c) => {
            let {
              ProductID,
              ProductName,
              Product_Value,
              donate_company,
              NonProfitName,
              CampaignName,
            } = c;
            console.log(
              ProductID,
              ProductName,
              Product_Value,
              donate_company,
              NonProfitName,
              CampaignName
            );
            return (
              <tbody>
                <tr>
                  {/* <th scope="row"></th> */}
                  <td>
                    <button onClick={() => GoToProduct(ProductID)}>
                      {ProductID}
                    </button>
                  </td>
                  <td>{ProductName}</td>
                  <td>{Product_Value}</td>
                  <td>{donate_company}</td>
                  <td>{NonProfitName}</td>
                  <td>{CampaignName}</td>
                  {/* <td>
                    {roles[0].name === "Social activist" ? (
                      <>
                        <button className="btn btn-success">
                          onClick=
                          {() => <PurchasePage />}
                          Buy
                        </button>
                        <button className="btn btn-primary">
                          onClick=
                          {() => <DonationsPage />}
                          Donation
                        </button>
                      </>
                    ) : (
                      <></>
                    )}
                  </td> */}
                </tr>

                {/* <td>
                  <Link
                    to="/donationPage"
                    onClick={() => {
                      SetProductArr(c);
                    }}
                  >
                    <button className="btn btn-danger">
                      Click here to donate
                    </button>
                  </Link>
                </td> */}
                <tr />
              </tbody>
            );
          })
        ) : (
          <tbody>
            <tr>
              <td colSpan={9}>
                <RingLoader className="spinner" color="#8d8de3" />
              </td>
            </tr>
          </tbody>
        )}
      </table>
    </div>
  );
};
