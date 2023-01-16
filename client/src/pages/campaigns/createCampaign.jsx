import React, { useState } from "react";
import { addCampaignToDb } from "../../services/services";
import "./createCampaign.css";

export const CreateCampaign = () => {
  const [addCampaign, SetAddCampaign] = useState({
    CampaignName: "",
    NonProfitName: "",
    Hashtag: "",
    Description: "",
  });

  const handleAddMessage = async () => {
    let json = addCampaign;
    await addCampaignToDb(json);
    SetAddCampaign({});
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  return (
    <div className="student-inputs ">
      <div className="input-group mb-3">
        <span className="input-group-text">Campaign name</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            SetAddCampaign({ ...addCampaign, CampaignName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">NonProfitName</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            SetAddCampaign({ ...addCampaign, NonProfitName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">Hashtag</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            SetAddCampaign({ ...addCampaign, Hashtag: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">Description</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            SetAddCampaign({ ...addCampaign, Description: o.target.value });
          }}
        />
      </div>
      <button className="btn btn-success" onClick={handleAddMessage}>
        Submit
      </button>
    </div>
  );
};
