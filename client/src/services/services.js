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
export const deleteCampaign = async (campaignId) => {
  try {
    console.log(campaignId);
    let Endpoint = `http://localhost:7255/api/campaign/DeleteCampaign/${campaignId}`;
    await axios.delete(Endpoint);
  } catch (error) {
    console.error(error);
  }
};
//edit campaign
export const updateCampaign = async (campaign, campaignId) => {
  try {
    await axios.put(
      `http://localhost:7255/api/campaign/UpdateCampaign/${campaignId}`,
      campaign
    );
    console.log(campaign, "pass");
    console.log(campaignId);
  } catch (error) {
    console.error(error);
  }
};

//twitter
export const GetTwitter = async () => {
  try {
    console.log("hi");
    let Endpoint = "http://localhost:7255/api/companies/getAcountData";
    let response = await axios.get(Endpoint);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
