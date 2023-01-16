import React, { useContext, useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { RingLoader } from "react-spinners";
import { GetCampaigns } from "../../services/services";

export const CampaignTbl = ({ setCampaign }) => {
  const [CampaignArr, SetCampaignArr] = useState(undefined);
  const navigate = useNavigate();
  let GetCampaignsData = async () => {
    let arrCampaign = await GetCampaigns();
    let arrCamp = Object.values(arrCampaign);
    SetCampaignArr(arrCamp);
  };

  useEffect(() => {
    GetCampaignsData();
  }, []);

  let GoTOCampaign = (CampaignID) => {
    console.log(CampaignID, "hallo");
    navigate("/campaignInfo", {
      state: {
        CampaignID,
      },
    });
  };
  return (
    <div>
      <table class="table table-dark table-hover">
        <thead>
          <tr>
            <th scope="col">CampaignID</th>
            <th scope="col">CampaignName</th>
            <th scope="col">NonProfitName</th>
            <th scope="col">Hashtag</th>
            <th scope="col">Description</th>
            {/* <th scope="col"></th> */}
          </tr>
        </thead>
        {CampaignArr && CampaignArr !== undefined ? (
          CampaignArr.map((c) => {
            let {
              CampaignID,
              CampaignName,
              NonProfitName,
              Hashtag,
              Description,
            } = c;
            console.log(
              CampaignID,
              CampaignName,
              NonProfitName,
              Hashtag,
              Description
            );
            return (
              <tbody>
                <tr>
                  {/* <th scope="row"></th> */}
                  <td>
                    <button onClick={() => GoTOCampaign(CampaignID)}>
                      {CampaignID}
                    </button>
                  </td>
                  <td>{CampaignName}</td>
                  <td>{NonProfitName}</td>
                  <td>{Hashtag}</td>
                  <td>{Description}</td>
                </tr>
                {/* <td>
                  <Link
                    to="/edit"
                    onClick={() => {
                      setCampaign(c);
                    }}
                  >
                    <button className="btn btn-danger">Edit</button>
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

// {CampaignArr && CampaignArr !== undefined ? (
//   CampaignArr.map((c)=>{
//     let {
// CampaignId,
// CampaignName,
// OrgId,
// Hashtag,
// Description,
//     }=c;
//     return (
//     <tbody>
//       <tr>
//       <th scope="row">1</th>
//       <td>
//         <Link to={`/cmapaignId:${campaign.CampaignId}`}
//         onClick={()=>{
//           setCampaign(c);
//         }}
//         >
//          { CampaignId}
//         </Link>
//       </td>
//       <td>{CampaignName}</td>
//       <td>{OrgId}</td>
//       <td>{Hashtag}</td>
//       <td>{Description}</td>
//       </tr>

//     )
//     }
//   })
// )}

//   <tr>
//     <th scope="row">2</th>
//     <td>Jacob</td>
//     <td>Thornton</td>
//     <td>@fat</td>
//   </tr>
//   <tr>
//     <th scope="row">3</th>
//     <td colspan="2">Larry the Bird</td>
//     <td>@twitter</td>
//   </tr>

//{
/* <div>
      <table class="table table-dark table-hover">
        <thead>
          <tr>
            <th scope="col">CampaignsID</th>
            <th scope="col">CampaignName</th>
            <th scope="col">OrgId</th>
            <th scope="col">Hashtag</th>
            <th scope="col">Description</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <th scope="row">1</th>
            <td>Jacob</td>
            <td>Thornton</td>
            <td>@fat</td>
          </tr>

          <tr>
            <th scope="row">2</th>
            <td>Jacob</td>
            <td>Thornton</td>
            <td>@fat</td>
          </tr>
          <tr>
            <th scope="row">3</th>
            <td colspan="2">Larry the Bird</td>
            <td>@twitter</td>
          </tr>
        </tbody>
      </table>
    </div> */
//}
