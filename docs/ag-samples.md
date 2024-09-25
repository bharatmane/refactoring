
# AG Grid Pivoting with Custom Aggregation and Filters in React

## Multi-Level Pivoting in AG Grid

Yes, AG Grid supports multi-level pivoting (also known as pivoting on multiple columns). This allows you to pivot on more than one field, so you can create a two-level pivot table where both fields become columns.

For example, you could pivot both by city (e.g., New York, Los Angeles, etc.) and another field, like region (e.g., Western, Eastern), so that both city and region values appear as columns, and the sales data is aggregated within those combinations.

### Example Code with Multi-Level Pivot:

```javascript
import React, { useState } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';

const CityRegionSalesPivotGrid = () => {
  const [rowData] = useState([
    { city: 'New York', region: 'Eastern', sales: 100 },
    { city: 'Los Angeles', region: 'Western', sales: 200 },
    { city: 'Chicago', region: 'Central', sales: 150 },
    { city: 'New York', region: 'Eastern', sales: 120 },
    { city: 'Los Angeles', region: 'Western', sales: 220 },
    { city: 'Chicago', region: 'Central', sales: 180 },
    { city: 'New York', region: 'Eastern', sales: 90 }
  ]);

  const [columnDefs] = useState([
    { headerName: 'City', field: 'city', pivot: true }, // First pivot on city
    { headerName: 'Region', field: 'region', pivot: true }, // Second pivot on region
    { headerName: 'Sales', field: 'sales', aggFunc: 'sum' } // Sales data aggregated using sum
  ]);

  const defaultColDef = {
    sortable: true,
    filter: true,
    resizable: true,
    flex: 1,
  };

  return (
    <div className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
      <AgGridReact
        rowData={rowData}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        pivotMode={true} // Enable pivot mode
      />
    </div>
  );
};

export default CityRegionSalesPivotGrid;
```

### Explanation:
- Pivot on Multiple Fields: We pivot on both the `city` and `region` columns by setting `pivot: true` on both fields.
- This creates a two-level pivot where the first level is city and the second level is region.
- The sales column is aggregated using the `sum` aggregation function. You can use other aggregation functions like `avg`, `min`, `max`, etc.

### Filtering with Multi-Level Pivoting:
You can apply filters with multiple pivot columns. For example:
- **City filter**: You can filter by `city` (e.g., only show data for `New York`).
- **Region filter**: You can filter by `region` (e.g., only show data for `Western` regions).

---

## Custom Aggregation Logic

AG Grid allows you to apply custom aggregation logic, meaning you can perform different types of aggregation for specific columns or even for specific values. If you want to sum the sales for certain cities in one column and other cities in another, you can achieve this by using a custom aggregation function.

### Example Code for Custom Aggregation:

```javascript
import React, { useState } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';

const CitySalesCustomAggregationGrid = () => {
  const [rowData] = useState([
    { city: 'New York', region: 'Eastern', sales: 100 },
    { city: 'Los Angeles', region: 'Western', sales: 200 },
    { city: 'Chicago', region: 'Central', sales: 150 },
    { city: 'New York', region: 'Eastern', sales: 120 },
    { city: 'Los Angeles', region: 'Western', sales: 220 },
    { city: 'Chicago', region: 'Central', sales: 180 },
    { city: 'New York', region: 'Eastern', sales: 90 }
  ]);

  const customAggregationNYLA = (params) => {
  let newYorkLosAngelesSum = 0;

  // Access the original row data
  if (params.node.allLeafChildren) {
    params.node.allLeafChildren.forEach((childNode) => {
      const value = childNode.data; // Original row data
      if (value.city === 'New York' || value.city === 'Los Angeles') {
        newYorkLosAngelesSum += value.sales;
      }
    });
  }

  return newYorkLosAngelesSum;
};

const customAggregationChicago = (params) => {
  let chicagoSum = 0;

  // Access the original row data
  if (params.node.allLeafChildren) {
    params.node.allLeafChildren.forEach((childNode) => {
      const value = childNode.data; // Original row data
      if (value.city === 'Chicago') {
        chicagoSum += value.sales;
      }
    });
  }

  return chicagoSum;
};



  const [columnDefs] = useState([
   {
    headerName: 'City', 
    field: 'city', 
    pivot: true // Pivot on city
  },
  {
    headerName: 'Region', 
    field: 'region', 
    pivot: true // Pivot on region
  },
  {
    headerName: 'Sales for NY/LA',
    field: 'sales',
    aggFunc: customAggregationNYLA, // Custom aggregation function for NY/LA
  },
  {
    headerName: 'Sales for Chicago',
    field: 'sales',
    aggFunc: customAggregationChicago, // Custom aggregation function for Chicago
  }
  ]);

  const defaultColDef = {
    sortable: true,
    filter: true,
    resizable: true,
    flex: 1,
  };

  return (
    <div className="ag-theme-alpine" style={{ height: 400, width: '100%' }}>
      <AgGridReact
        rowData={rowData}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        pivotMode={true} // Enable pivot mode
      />
    </div>
  );
};

{
    headerName: 'City', 
    field: 'city', 
    rowGroup: true, // Group by city to aggregate dynamically
    hide: true // Hide the city column in the main grid, as it will be displayed as a group
  },
  {
    headerName: 'Total Sales',
    field: 'sales',
    aggFunc: 'sum', // Aggregate sales per city
  },
  {
    headerName: 'Sales for NY+LA',
    field: 'sales',
    valueGetter: (params) => {
      if (params.node.group && (params.node.key === 'New York' || params.node.key === 'Los Angeles')) {
        return params.node.aggData ? params.node.aggData.sales : 0;
      }
      return 0;
    },
  },
  {
    headerName: 'Sales for Chicago',
    field: 'sales',
    valueGetter: (params) => {
      if (params.node.group && params.node.key === 'Chicago') {
        return params.node.aggData ? params.node.aggData.sales : 0;
      }
      return 0;
    },
  },


<AgGridReact
        rowData={rowData} // Pass in streamed data directly
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        groupIncludeFooter={true} // Include footer for groups to show aggregated totals
        animateRows={true} // Smoothly update rows as new data is streamed in
      />
export default CitySalesCustomAggregationGrid;
```

### Explanation:
- We use a custom aggregation function that aggregates the sales for **New York** and **Los Angeles** together, while **Chicago** has its own sum.
- This allows us to display the sums in separate columns based on city, even though the data is coming from a single sales field.

---

## Filtering and Pivot Together

Yes, you can apply both filtering and pivoting at the same time in AG Grid. The grid will filter the data first and then apply the pivot and aggregation logic.

- For example, you can filter rows where `sales > 150` before applying the pivot. The grid will only show the data that matches the filter criteria.

---

## Conclusion

AG Grid is highly flexible and powerful, supporting advanced features like multi-level pivoting, custom aggregation logic, and combined filtering with pivoting.
