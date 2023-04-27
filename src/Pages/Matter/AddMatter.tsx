import { useEffect, useState } from "react";
import { Matter } from "../../Data/Matter";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { Jurisdiction } from "../../Data/Jurisdiction";
import { Attorney } from "../../Data/Attorney";
import { Client } from "../../Data/Client";
import { baseUrl } from "../../App";

const AddMatter: React.FC = () => {
  const [newMatter, setNewMatter] = useState<Matter>({
    id: 0,
    name: '',
    clientId: 0,
    jurisdictionId: 0,
    billingAttorneyId: 0,
    responsibleAttorneyId: 0
  });
  const [jurisdictions, setJurisdictions] = useState<Jurisdiction[]>([]);
  const [attorneys, setAttorneys] = useState<Attorney[]>([]);
  const [clients, setClients] = useState<Client[]>([]);

  const getJurisdictions = () => {
    axios.get(`${baseUrl}/jurisdictions`)
      .then(res => {
        setJurisdictions(res.data.result)
      })
      .catch(err => {
        console.log(err)
      });
  }

  const getAttorneys = (id: number) => {
    axios.get(`${baseUrl}/attorney/${id}/attorneys`)
      .then(res => {
        setAttorneys(res.data.result)
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
    getJurisdictions();
    getClients();
  }, [])

  const navigate = useNavigate();

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setNewMatter({ ...newMatter, [name]: value });
  }

  const handleDropdownChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = event.target;
    setNewMatter({ ...newMatter, [name]: value });
  }

  const handleJurisdictionChnage = (event: any) => {
    const { name, value } = event.target;
    setNewMatter({ ...newMatter, [name]: value });
    getAttorneys(value);
  }

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    axios
      .post(`http://localhost:5274/api/matter`, newMatter)
      .then(res => {
        navigate('/ViewCustomer')
      })
      .catch(err => {
        console.log(err)
      });
  }

  return (
    <div className="container border p3 my-3">
      <form className="row g-3 p-3" onSubmit={handleSubmit} autoComplete="off">
        {/* {loading &&
        <Loader />
      } */}
        <div className="form-header h2 text-center bg-light">
          Create a new Matter
        </div>
        <div className="row">
          <div className="col-md-6">
            <label htmlFor="name" className="form-label">Matter Name</label>
            <input
              type="text"
              className="form-control"
              placeholder="Matter name"
              id='name'
              aria-label="Matter Name"
              name="name"
              value={newMatter.name}
              onChange={handleChange}
              required />
            {newMatter.name == "" ? <p className="text-danger font-weight-bold">* Please enter the name</p> : null}
          </div>
          <div className="col-md-6">
            <label htmlFor="client-dropdown" className="form-label">Clients</label>
            <select
              required
              id="client-dropdown"
              className="form-select"
              name="clientId"
              defaultValue="Select-Client"
              onChange={handleDropdownChange}
            >
              <option value="Select-Client" disabled>Select Client</option>
              {clients.map((item) => {
                return (
                  <option key={item.id} value={item.id} >
                    {item.name}
                  </option>
                );
              })}
            </select>
            {newMatter.clientId == 0 ? <p className="text-danger font-weight-bold">* Please select the client</p> : null}
          </div>
        </div>
        <div className="row">
          <div className="col-md-6">
            <label htmlFor="jurisdiction-dropdown" className="form-label">Jurisdiction Name</label>
            <select
              required
              id="jurisdiction-dropdown"
              className="form-select"
              name="jurisdictionId"
              defaultValue="Select-Area"
              onChange={handleJurisdictionChnage}
            >
              <option value="Select-Area" disabled>Select Jurisdiction Area</option>
              {jurisdictions && jurisdictions.map((item) => {
                return (
                  <option key={item.id} value={item.id} >
                    {item.name}
                  </option>
                );
              })}
            </select>
            {newMatter.jurisdictionId == 0 ? <p className="text-danger font-weight-bold">* Please select the jurisdiction area</p> : null}
          </div>
          <div className="col-md-6">
            <label htmlFor="billingAttorney" className="form-label">Billing Attorney Name</label>
            <select
              required
              id="billingAttorney-dropdown"
              className="form-select"
              name="billingAttorneyId"
              defaultValue="Select billing attorney"
              onChange={handleDropdownChange}
            >
              <option value="Select billing attorney" disabled>Select billing attorney</option>
              {attorneys && attorneys.map((item) => {
                return (
                  <option key={item.id} value={item.id} >
                    {item.name}
                  </option>
                );
              })}
            </select>
            {newMatter.billingAttorneyId == 0 && attorneys.length != 0 ? <p className="text-danger font-weight-bold">* Please select the billing attorney</p> : null}
          </div>
        </div>
        <div className="row">
        <div className="col-md-6">
            <label htmlFor="responsibleAttorney" className="form-label">Responsible Attorney</label>
            <select
              required
              id="responsibleAttorney-dropdown"
              className="form-select"
              name="responsibleAttorneyId"
              defaultValue="Select responsible attorney"
              onChange={handleDropdownChange}
            >
              <option value="Select responsible attorney" disabled>Select responsible attorney</option>
              {attorneys && attorneys.map((item) => {
                return (
                  <option key={item.id} value={item.id} >
                    {item.name}
                  </option>
                );
              })}
            </select>
            {newMatter.responsibleAttorneyId == 0 && attorneys.length != 0 ? <p className="text-danger font-weight-bold">* Please select the responsible attorney</p> : null}
          </div>
        </div>


        <div className="col-12">
          <input
            type="submit"
            className="btn btn-primary"
            value="Add Matter"
          />
        </div>
      </form>
    </div>
  );
}

export default AddMatter;