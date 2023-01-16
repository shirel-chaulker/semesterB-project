import React, { useEffect, useState } from "react";
import { GetDelivery } from "../../services/userServices";
import { Link } from "react-router-dom";
import { RingLoader } from "react-spinners";

export const DeliveryReport = (props) => {
  const [DeliveryArr, SetDeliveryArr] = useState();

  let GetDeliveryData = async () => {
    let delivery = await GetDelivery();
    let arrDelivery = Object.values(delivery);
    SetDeliveryArr(arrDelivery);
  };

  useEffect(() => {
    GetDeliveryData();
  }, []);

  return (
    <div className="card">
      {DeliveryArr && DeliveryArr !== undefined ? (
        DeliveryArr.map((d) => {
          let { ProductName, FullName, Address, PhoneNumber } = d;
          return (
            <div>
              <div className="card-body">
                {<h5 className="card-title">{ProductName}</h5>}
              </div>
              <ul className="list-group list-group-flush">
                {<li className="list-group-item">{FullName}</li>}
                {<li className="list-group-item">{Address}</li>}
                {<li className="list-group-item">{PhoneNumber}</li>}
                <li className="list-group-item">
                  <button type="button" className="btn btn-info">
                    <Link to="/" className="navbar_links">
                      return
                    </Link>
                  </button>
                </li>
              </ul>
            </div>
          );
        })
      ) : (
        <div colSpan={9}>
          <RingLoader className="spinner" color="#8d8de3" />
        </div>
      )}
    </div>
  );
};
