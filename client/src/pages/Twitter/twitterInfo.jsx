import React, { useEffect, useState } from "react";
import { GetTwitter } from "../../services/services";
import { RingLoader } from "react-spinners";

export const TwitterInfo = () => {
  const [TwitterArr, SetTwitterArr] = useState();

  let GetTwitterData = async () => {
    let twitt = await GetTwitter();
    let result = Object.values(twitt);
    SetTwitterArr(result);
  };

  useEffect(() => {
    GetTwitterData();
  });

  return (
    <div>
      <table class="table table-dark table-hover">
        <thead>
          <tr>
            <th scope="col">TrackID</th>
            <th scope="col">EarnMoney</th>
            <th scope="col">CampaignID</th>
            <th scope="col">Hashtag</th>
            <th scope="col">TwitterAcount</th>
          </tr>
        </thead>
        {TwitterArr && TwitterArr !== undefined ? (
          TwitterArr.map((t) => {
            let { TrackID, EarnMoney, CampaignID, Hashtag, TwitterAcount } = t;

            return (
              <tbody>
                <tr>
                  {/* <th scope="row"></th> */}
                  <td>{TrackID}</td>
                  <td>{EarnMoney}</td>
                  <td>{CampaignID}</td>
                  <td>{Hashtag}</td>
                  <td>{TwitterAcount}</td>
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
