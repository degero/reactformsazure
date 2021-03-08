import React from 'react';

import { Formik, Form, Field, ErrorMessage } from 'formik';
import { post } from '../utils/Http';
import { ApiPath, UrlPathMap } from '../utils/Settings';

const DetailsForm = () => (
  <div>
    <h1>User details</h1>
    <Formik
      initialValues={{ email: '', name: '' }}
      validate={values => {
        const errors:any = {};
        if (!values.email) {
          errors.email = 'Required';
        } else if (
          !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)
        ) {
          errors.email = 'Invalid email address';
        }
        return errors;
      }}
      onSubmit={(values, { setSubmitting }) => {
        setTimeout(() => {
          post(ApiPath + UrlPathMap['saveuser'], {body: JSON.stringify(values)});
          setSubmitting(false);
        }, 400);
      }}
    >
      {({ isSubmitting }) => (
        <Form>
            <label>Name:</label>
            <Field type="text" name="name" />
            <ErrorMessage name="name" component="div" />
            <label>Email:</label>
            <Field type="email" name="email" />
            <ErrorMessage name="email" component="div" />
            <button type="submit" disabled={isSubmitting}>
                Submit
            </button>
        </Form>
      )}
    </Formik>
  </div>
);

export default DetailsForm;