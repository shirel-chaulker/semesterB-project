import axios from "axios";

//get all the companies from db
export const GetCompanies = async () => {
  console.log("hi");
  let Endpoint = "http://localhost:7255/api/companies/getCompanies";
  let response = await axios.get(Endpoint);
  //console.log(response.data);
  return response.data;
};

// report company
export const GetDelivery = async () => {
  console.log("hi");
  let Endpoint = "http://localhost:7255/api/companies/getReport";
  let response = await axios.get(Endpoint);
  //console.log(response.data);
  return response.data;
};

//get all the products from db

export const GetProducts = async () => {
  let Endpoint = "http://localhost:7255/api/product/Get";
  let response = await axios.get(Endpoint);
  return response.data;
};

//get product by id
export const getProductById = async (ProductID) => {
  try {
    let endpoint = `http://localhost:7255/api/product/GetByID/${ProductID}`;
    let response = await axios.get(endpoint);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

//add product to db(donation by company)

export const AddProductToDB = async (newProduct) => {
  try {
    let Endpoint = "http://localhost:7255/api/product/Post";
    let response = await axios.post(Endpoint, newProduct);
    console.log(newProduct);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

export const AddPurchaseToDB = async (newPurchase) => {
  try {
    let Endpoint = "http://localhost:7255/api/product/PostPurchase";
    let response = await axios.post(Endpoint, newPurchase);
    console.log(newPurchase);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

// get all social activist from db
export const GetActivists = async () => {
  let Endpoint = "http://localhost:7255/api/activist/Get";
  let response = await axios.get(Endpoint);
  return response.data;
};

//add activist to db(sign up by the user)

export const AddActivistToDB = async (newActivist) => {
  try {
    let Endpoint = "http://localhost:7255/api/activist/Post";
    let response = await axios.post(Endpoint, newActivist);
    console.log(newActivist);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

//Get all non profit rep
export const GetNonProfitRep = async () => {
  let Endpoint = "http://localhost:7255/api/nonProfit/Get";
  let response = await axios.get(Endpoint);
  return response.data;
};

export const getOrgById = async (OrgID) => {
  try {
    let endpoint = `http://localhost:7255/api/nonProfit/GetByID/${OrgID}`;
    let response = await axios.get(endpoint);
    console.log(response.data);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

//take user data response from client to db
export const AddProfitRepToDB = async (newNonProfit) => {
  try {
    let Endpoint = "http://localhost:7255/api/nonProfit/Post";
    let response = await axios.post(Endpoint, newNonProfit);
    console.log(newNonProfit);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
