import React, { useEffect, useState } from "react";
import { MatterInvoice } from "../../Data/MatterInvoiceData";
import axios from "axios";
import "../../Assets/Styles/table.css"
import { Attorney } from "../../Data/Attorney";
import { baseUrl } from "../../App";

const ViewInvoicesByAttorney: React.FC = () => {
  const [attorneyInvoiceGroupData, setAttorneyInvoiceGroupData] = useState<MatterInvoice[][] | null>();
  const [attorneyInvoiceData, setAttorneyInvoiceData] = useState<MatterInvoice[] | null>(null);
  const [attorneys, setAttorneys] = useState<Attorney[]>();

  const getInvoicesByAttorneys = () => {
    axios.get(`${baseUrl}/invoicesBillingByAttorney`)
      .then(res => {
        setAttorneyInvoiceGroupData(res.data.result);
        setAttorneyInvoiceData(null);
      })
      .catch(err => {
        console.log(err)
      });
  }

  const getAttorneyInvoices = (value: number) => {
    axios.get(`${baseUrl}/attorneys/${value}/invoices`)
      .then(res => {
        setAttorneyInvoiceData(res.data.result);
        setAttorneyInvoiceGroupData(null);
      })
      .catch(err => {
        console.log(err)
      });
  }

  const getAttorneys = () => {
    axios.get(`${baseUrl}/attorneys`)
      .then(res => {
        setAttorneys(res.data.result)
      })
      .catch(err => {
        console.log(err)
      });
  }

  useEffect(() => {
    getInvoicesByAttorneys();
    getAttorneys();
  }, []);

  const handleClientChange = (event: any) => {
    event.preventDefault();
    const { name, value } = event.target
    if (value == 'All') {
      getInvoicesByAttorneys();
    }
    else {
      getAttorneyInvoices(event.target.value);
    }
  }
  return (
    <>
      <div className="row">
        <div className="col-md-3">
          <label htmlFor="attorney-dropdown" className="form-label">Filter By attorney</label>
          <select
            required
            id="attorney-dropdown"
            className="form-select"
            name="attorneyId"
            defaultValue="All"
            onChange={handleClientChange}
          >
            <option value="All">All</option>
            {attorneys?.map((item) => {
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
                {attorneyInvoiceGroupData != null ? <th></th> : null}
                <th>Id
                </th>
                <th>Client Name
                </th>
                <th>Attorney Name
                </th>
                <th>Rate per hour
                </th>
                <th>Time spent</th>
                <th>Total Amount</th>
                <th>Date</th>
              </tr>
            </thead>
            <tbody>
              {attorneyInvoiceGroupData && attorneyInvoiceGroupData?.map((attorneyName, index) => {
                return (
                  <React.Fragment key={index}>
                    <tr>
                      <th rowSpan={attorneyInvoiceGroupData[index].length + 1}>Attorney #{index + 1}</th>
                      <th colSpan={5}>{attorneyInvoiceGroupData[index][0].attorneyName}</th>
                    </tr>
                    {attorneyInvoiceGroupData[index].map((attorney, index) => {
                      return (
                        <tr key={index}>
                          <td>{index + 1}</td>
                          <td>{attorney.clientName}</td>
                          <td>{attorney.attorneyName}</td>
                          <td>{attorney.ratePerHour}</td>
                          <td>{attorney.timeSpent}</td>
                          <td>{attorney.totalAmount}</td>
                          <td>{new Date(attorney.date).toLocaleDateString()}</td>
                        </tr>
                      )
                    })}
                  </React.Fragment>
                )
              })}
              {attorneyInvoiceData && attorneyInvoiceData?.map((attorney, index) => {
                return (
                  <tr key={index}>
                    <td>{index + 1}</td>
                    <td>{attorney.clientName}</td>
                    <td>{attorney.attorneyName}</td>
                    <td>{attorney.ratePerHour}</td>
                    <td>{attorney.timeSpent}</td>
                    <td>{attorney.totalAmount}</td>
                    <td>{new Date(attorney.date).toLocaleDateString()}</td>
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

export default ViewInvoicesByAttorney;