import React from 'react';
import { Route } from 'react-router';
import  Layout  from './components/Layout';
import Home from './pages/Home';
import Generate from './pages/Generate';
import Upload from './pages/Upload';

export default function App() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/generate' component={Generate} />
        <Route exact path='/upload' component={Upload} />
      </Layout>
    );
  }

