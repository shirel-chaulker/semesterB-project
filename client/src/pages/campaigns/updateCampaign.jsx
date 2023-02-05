import React, { useState } from "react";
import { updateCampaign } from "../../services/services";

export const UpdateCampaign = ({ campaign }) => {
  console.log(campaign);
  const initValue = {
    CampaignName: "",
    NonProfitName: "",
    Hashtag: "",
    Description: "",
  };

  const [editCampaign, setEditCampaign] = useState(initValue);

  const handleUpdateCampaign = async () => {
    campaign.CampaignName = editCampaign.CampaignName;
    campaign.NonProfitName = editCampaign.NonProfitName;
    campaign.Hashtag = editCampaign.Hashtag;
    campaign.Description = editCampaign.Description;

    await updateCampaign(campaign, campaign.CampaignID);
    // setEditCampaign({});
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  return (
    <div className="student-inputs ">
      <div className="input-group mb-3">
        <span className="input-group-text" id="inputGroup-sizing-default">
          Campaign Name
        </span>
        <input
          className="form-control"
          type="text"
          aria-label="default input example"
          // value={campaign.CampaignName}
          onChange={(o) => {
            setEditCampaign({ ...editCampaign, CampaignName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text" id="inputGroup-sizing-default">
          Non Profit Name
        </span>
        <input
          className="form-control"
          type="text"
          aria-label="default input example"
          // value={campaign.NonProfitName}
          onChange={(o) => {
            setEditCampaign({ ...editCampaign, NonProfitName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text" id="inputGroup-sizing-default">
          Hashtag
        </span>
        <input
          className="form-control"
          type="text"
          aria-label="default input example"
          //value={campaign.Hashtag}
          onChange={(o) => {
            setEditCampaign({ ...editCampaign, Hashtag: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text" id="inputGroup-sizing-default">
          Description
        </span>
        <input
          className="form-control"
          type="text"
          aria-label="default input example"
          //value={campaign.Description}
          onChange={(o) => {
            setEditCampaign({ ...editCampaign, Description: o.target.value });
          }}
        />
      </div>
      <button className="btn btn-secondary" onClick={handleUpdateCampaign}>
        Update capaign
      </button>
    </div>
  );
};
