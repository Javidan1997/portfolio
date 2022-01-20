#!/usr/bin/env python
# coding: utf-8

# In[1]:


get_ipython().run_line_magic('matplotlib', 'inline')
import matplotlib.pyplot as plt
import matplotlib as mpl
import pandas as pd
import seaborn as sns
import sklearn
import numpy as np


# In[7]:


df = pd.read_csv(r"C:\Users\abdullayevc\Desktop\challenge_dataset.txt", names=['X','Y'])


# In[8]:


sns.regplot(x='X', y='Y', data=df, fit_reg=False)
plt.show()


# In[12]:


from sklearn.model_selection import train_test_split

X_train, X_test, y_train, y_test = np.asarray(train_test_split(df['X'], df['Y'], test_size=0.1))


# In[13]:


from sklearn.linear_model import LinearRegression

reg = LinearRegression()
reg.fit(X_train.values.reshape(-1,1), y_train.values.reshape(-1,1))


# In[16]:


print('score', reg.score(X_test.values.reshape(-1,1),y_test.values.reshape(-1,1)))


# In[17]:


x_line = np.arange(5,25).reshape(-1,1)
sns.regplot(x=df['X'], y=df['Y'], data=df, fit_reg=False)
plt.plot(x_line, reg.predict(x_line))
plt.show()


# In[22]:


co2_df = pd.read_csv('C:/Users/abdullayevc/Desktop/global_co2.csv')
temp_df = pd.read_csv('C:/Users/abdullayevc/Desktop/annual_temp.csv')
print(co2_df.head())
print(temp_df.head())


# In[41]:



co2_df = co2_df.loc[co2_df['Year'] >= 1960] 
co2_df.columns=['Year','CO2']               
co2_df = co2_df.reset_index(drop=True)              

temp_df = temp_df[temp_df.Source != 'GISTEMP']                             
temp_df.drop('Source', inplace=True, axis=1)                                
temp_df = temp_df.reindex(index=temp_df.index[::-1])                       
temp_df = temp_df.loc[temp_df['Year'] >= 1960].loc[temp_df['Year'] <= 2010]  
temp_df.columns=['Year','Temperature']                                     
temp_df = temp_df.reset_index(drop=True)                                          

print(co2_df.head())
print(temp_df.head())


# In[42]:


climate_change_df = pd.concat([co2_df, temp_df.Temperature], axis=1)

print(climate_change_df.head())


# In[43]:


from mpl_toolkits.mplot3d import Axes3D
fig = plt.figure()
fig.set_size_inches(12.5, 7.5)
ax = fig.add_subplot(111, projection='3d')

ax.scatter(xs=climate_change_df['Year'], ys=climate_change_df['Temperature'], zs=climate_change_df['CO2'])

ax.set_ylabel('Relative tempature'); ax.set_xlabel('Year'); ax.set_zlabel('CO2 Emissions')
ax.view_init(10, -45)


# In[44]:


f, axarr = plt.subplots(2, sharex=True)
f.set_size_inches(12.5, 7.5)
axarr[0].plot(climate_change_df['Year'], climate_change_df['CO2'])
axarr[0].set_ylabel('CO2 Emissions')
axarr[1].plot(climate_change_df['Year'], climate_change_df['Temperature'])
axarr[1].set_xlabel('Year')
axarr[1].set_ylabel('Relative temperature')


# In[58]:



X = climate_change_df[['Year']].to_numpy() 
Y = climate_change_df[['CO2','Temperature']].to_numpy().astype('float32')

X_train, X_test, y_train, y_test = np.asarray(train_test_split(X, Y, test_size=0.1))


# In[59]:


reg = LinearRegression()
reg.fit(X_train, y_train)


# In[60]:


print('Score: ', reg.score(X_test.reshape(-1, 1), y_test))


# In[61]:


x_line = np.arange(1960,2011).reshape(-1,1)
p = reg.predict(x_line).T


# In[ ]:




