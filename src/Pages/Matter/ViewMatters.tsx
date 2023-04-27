import React, { useContext, useEffect, useState } from "react";
import { ClientMatter } from "../../Data/ClientMatterData";
import axios from "axios";
import "../../Assets/Styles/table.css"
import { Client } from "../../Data/Client";
import { AppContext, baseUrl } from "../../App";
import Loader from "../../Components/Loader";

const ViewMatters: React.FC = () => {
  const { loading } = useContext(AppContext);

  const [clientMatterGroupData, setClientMatterGroupData] = useState<ClientMatter[][] | null>();
  const [clientMatterData, setClientMatterData] = useState<ClientMatter[] | null>(null);
  const [clients, setClients] = useState<Client[]>();

  const getMattersByClient = () => {
    axios.get(`${baseUrl}/mattersByClients`)
      .then(res => {
        setClientMatterGroupData(res.data.result);
        setClientMatterData(null);
      })
      .catch(err => {
        console.log(err)
      });
  }

  const getClientMatters = (value: number) => {
    axios.get(`${baseUrl}/matters/${value}/matters`)
      .then(res => {
        setClientMatterData(res.data.result);
        setClientMatterGroupData(null);
      })
      .catch(err => {
        console.log(err)
      });
  }

  const getClients = () => {
    axios.get(`${baseUrl}/clients`)
      .then(res => {
        setClients(res.data.result)
      })
      .catch(err => {
        console.log(err)
      });
  }

  useEffect(() => {
    getMattersByClient();
    getClients();
  }, []);

  const handleClientChange = (event: any) => {
    event.preventDefault();
    const { name, value } = event.target
    if (value == 'All') {
      getMattersByClient();
    }
    else {
      getClientMatters(event.target.value);
    }
  }
  return (
    <>
      {loading &&
        <Loader />
      }      <div className="row">
        <div className="col-md-3">
          <label htmlFor="client-dropdown" className="form-label">Filter By Client</label>
          <select
            required
            id="client-dropdown"
            className="form-select"
            name="clientId"
            defaultValue="All"
            onChange={handleClientChange}
          >
            <option value="All">All</option>
            {clients?.map((item) => {
              return (
                <option key={item.id} value={item.id} >
                  {item.name}
                </option>
              );
            })}
          </select>
        </div>
      </div>
      <div id="table-wrapper">
        <div id="table-scroll">
          <table className="table table-hover table-bordered">
            <thead className="table-dark">
              <tr className='bg-light sticky-top'>
                {clientMatterGroupData != null ? <th></th> : null}
                <th>Id
                </th>
                <th>Matter Name
                </th>
                <th>Billing Attorney
                </th>
                <th>Responsible Attorney
                </th>
                <th >Jurisdiction</th>
              </tr>
            </thead>
            <tbody>
              {clientMatterGroupData && clientMatterGroupData?.map((clientName, index) => {
                return (
                  <React.Fragment key={index}>
                    <tr>
                      <th rowSpan={clientMatterGroupData[index].length + 1}>Client #{index + 1}</th>
                      <th colSpan={5}>{clientMatterGroupData[index][0].clientName}</th>
                    </tr>
                    {clientMatterGroupData[index].map((matter, index) => {
                      return (
                        <tr key={index}>
                          <td>{index + 1}</td>
                          <td>{matter.name}</td>
                          <td>{matter.billingAttorneyName}</td>
                          <td>{matter.responsibleAttorneyName}</td>
                          <td>{matter.jurisdictionName}</td>
                        </tr>
                      )
                    })}
                  </React.Fragment>
                )
              })}
              {clientMatterData && clientMatterData?.map((matter, index) => {
                return (
                  <tr key={index}>
                    <td>{index + 1}</td>
                    <td>{matter.name}</td>
                    <td>{matter.billingAttorneyName}</td>
                    <td>{matter.responsibleAttorneyName}</td>
                    <td>{matter.jurisdictionName}</td>
                  </tr>
                )
              })}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
}

export default ViewMatters;