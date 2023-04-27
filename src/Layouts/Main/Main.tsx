import { Routes, Route } from "react-router-dom";
import AddMatter from "../../Pages/Matter/AddMatter";
import ViewMatters from "../../Pages/Matter/ViewMatters";
import ViewInvoices from "../../Pages/Invoice/ViewInvoices";
import ViewInvoicesByAttorney from "../../Pages/Invoice/ViewInvoicesByAttorney";

function Main() {

  return (
    <div>
      <Routes>
      <Route path="/AddMatter" element={<AddMatter />} />
      <Route path="/ViewMatters" element={<ViewMatters />} />
      <Route path="/ViewInvoices" element={<ViewInvoices />} />
      <Route path="/ViewInvoicesByAttorney" element={<ViewInvoicesByAttorney />} />
      </Routes>
    </div>
  );
}

export default Main;