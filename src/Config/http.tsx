import axios from 'axios';
import React, { useContext, useState } from 'react';
import { AppContext } from '../App';
import { Spinner } from 'react-bootstrap';

let loading = false;

axios.interceptors.request.use(
  (config) => {
    loading = false; 
    return config;
  },
  (error) => {
    loading = false; 
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(
  (response) => {
    loading = false; 
    return response;
  },
  (error) => {
    loading = false; 
    return Promise.reject(error);
  }
);

export const setLoading = (status: boolean) => {
  loading = status;
};

export const getLoading = () => {
  return loading;
}