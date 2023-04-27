import React, { useEffect, useState } from "react";
import { MatterInvoice } from "../../Data/MatterInvoiceData";
import axios from "axios";
import "../../Assets/Styles/table.css"
import { Matter } from "../../Data/Matter";
import { baseUrl } from "../../App";

const ViewInvoices: React.FC = () => {
  const [matterInvoiceGroupData, setMatterInvoiceGroupData] = useState<MatterInvoice[][] | null>();
  const [matterInvoiceData, setMatterInvoiceData] = useState<MatterInvoice[] | null>(null);
  const [matters, setMatters] = useState<Matter[]>();

  const getInvoicesByMatters = () => {
    axios.get(`${baseUrl}/invoicesByMatters`)
      .then(res => {
        setMatterInvoiceGroupData(res.data.result);
        setMatterInvoiceData(null);
      })
      .catch(err => {
        console.log(err)
      });
  }

  const getMatterInvoices = (value: number) => {
    axios.get(`${baseUrl}/matters/${value}/invoices`)
      .then(res => {
        setMatterInvoiceData(res.data.result);
        setMatterInvoiceGroupData(null);
      })
      .catch(err => {
        console.log(err)
      });
  }

  const getMatters = () => {
    axios.get(`${baseUrl}/matters`)
      .then(res => {
        setMatters(res.data.result)
      })
      .catch(err => {
        console.log(err)
      });
  }

  useEffect(() => {
    getInvoicesByMatters();
    getMatters();
  }, []);

  const handleClientChange = (event: any) => {
    event.preventDefault();
    const { name, value } = event.target
    if (value == 'All') {
      getInvoicesByMatters();
    }
    else {
      getMatterInvoices(event.target.value);
    }
  }
  return (
    <>
      <div className="row">
        <div className="col-md-3">
          <label htmlFor="matter-dropdown" className="form-label">Filter By matter</label>
          <select
            required
            id="matter-dropdown"
            className="form-select"
            name="matterId"
            defaultValue="All"
            onChange={handleClientChange}
          >
            <option value="All">All</option>
            {matters?.map((item) => {
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
                {matterInvoiceGroupData != null ? <th></th> : null}
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
              {matterInvoiceGroupData && matterInvoiceGroupData?.map((matterName, index) => {
                return (
                  <React.Fragment key={index}>
                    <tr>
                      <th rowSpan={matterInvoiceGroupData[index].length + 1}>Matter #{index + 1}</th>
                      <th colSpan={5}>{matterInvoiceGroupData[index][0].matterName}</th>
                    </tr>
                    {matterInvoiceGroupData[index].map((matter, index) => {
                      return (
                        <tr key={index}>
                          <td>{index + 1}</td>
                          <td>{matter.clientName}</td>
                          <td>{matter.attorneyName}</td>
                          <td>{matter.ratePerHour}</td>
                          <td>{matter.timeSpent}</td>
                          <td>{matter.totalAmount}</td>
                          <td>{new Date(matter.date).toLocaleDateString()}</td>
                        </tr>
                      )
                    })}
                  </React.Fragment>
                )
              })}
              {matterInvoiceData && matterInvoiceData?.map((matter, index) => {
                return (
                  <tr key={index}>
                    <td>{index + 1}</td>
                    <td>{matter.clientName}</td>
                    <td>{matter.attorneyName}</td>
                    <td>{matter.ratePerHour}</td>
                    <td>{matter.timeSpent}</td>
                    <td>{matter.totalAmount}</td>
                    <td>{new Date(matter.date).toLocaleDateString()}</td>
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

export default ViewInvoices;