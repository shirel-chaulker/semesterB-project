import axios from "axios";

export const getRoles = async (userId) => {
  let result = await axios.get(`http://localhost:7255/api/roles/${userId}`);
  console.log("response", result);
  if (result.status === 200) {
    return result.data;
  } else {
    return {};
  }
};
//add campaign to db
export const addCampaignToDb = async (NewCampaign) => {
  try {
    console.log(NewCampaign);
    let endpoint = "http://localhost:7255/api/campaign/Add";
    await axios.post(endpoint, NewCampaign);
  } catch (error) {
    console.error(error);
  }
};

//get all campaigns
export const GetCampaigns = async () => {
  console.log("hi");
  let Endpoint = "http://localhost:7255/api/campaign/Get";
  let response = await axios.get(Endpoint);
  console.log(response.data);
  return response.data;
};
//get campaign by id
export const getCampaignById = async (CampaignId) => {
  try {
    let endpoint = `http://localhost:7255/api/campaign/GetByID/${CampaignId}`;
    let response = await axios.get(endpoint);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
//delete campaign
export const DeleteCampaign = async (campaignId) => {
  let Endpoint = `http://localhost:7255/api/campaign/DeleteCampaign/${campaignId}`;
  await axios.delete(Endpoint);
};
//edit campaign
export const UpdateCampaign = async (campaign, campaignId) => {
  await axios.put(
    `http://localhost:7255/api/campaign/UpdateCampaign/${campaignId}`,
    campaign
  );
};

//twitter
export const GetTwitter = async () => {
  console.log("hi");
  let Endpoint = "http://localhost:7255/api/Twitter/getTwitt";
  let response = await axios.get(Endpoint);
  console.log(response.data);
  return response.data;
};
