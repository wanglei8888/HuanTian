<template>
  <a-card :bordered="false" v-show="codeGenDetailShow">
    <div class="table-operator">
      <a-button class="but_item" type="dashed" @click="handleCancel" icon="rollback">返回</a-button>
      <a-button type="primary" icon="plus" @click="handleSubmit">保存</a-button>
    </div>
    <a-alert message="该功能只适配了MySql数据库,需要其他数据库可提Issues" banner closable type="info" how-icon />
    <a-table
      ref="table"
      size="middle"
      :columns="columns"
      :dataSource="loadData"
      :pagination="false"
      :alert="true"
      :loading="tableLoading"
      :rowKey="(record) => record.id">
      <span slot="columnDescriptionTitle">
        <a-tooltip title="代码生成的名字都为此值">
          <a-icon type="smile-o" />
        </a-tooltip>&nbsp;
        描述
      </span>
      <span slot="queryParametersTitle">
        <a-tooltip title="值为true的时候,页面列表查询会显示此列">
          <a-icon type="question-circle-o" />
        </a-tooltip>&nbsp;
        列表查询
      </span>
      <template slot="columnDescription" slot-scope="text, record">
        <a-input v-model="record.columnDescription" />
      </template>
      <template slot="order" slot-scope="text, record">
        <a-input-number v-model="record.order" />
      </template>
      <template slot="frontendType" slot-scope="text, record, indexs">
        <a-select style="width: 180px" v-model="record.frontendType" :disabled="false" @change="(e) => frontendTypeChange(record)">
          <a-select-option v-for="(item, index) in frontendTypeData" :key="index" :value="item.value">{{
            item.name
          }}</a-select-option>
        </a-select>
      </template>
      <template slot="dropDownCode" slot-scope="text, record">
        <a-select
          show-search
          :filter-option="filterOption"
          style="width: 180px"
          v-model="record.dropDownCode"
          :disabled="record.frontendType != 'dropDown'" >
          <a-select-option v-for="(item, index) in dropDownCodeData" :key="index" :value="item.code">{{
            item.name
          }}</a-select-option>
        </a-select>
      </template>
      <template slot="required" slot-scope="text, record">
        <a-checkbox v-model="record.required" :disabled="false" />
      </template>
      <template slot="queryParameters" slot-scope="text, record">
        <a-switch v-model="record.queryParameters">
          <a-icon slot="checkedChildren" type="check" />
          <a-icon slot="unCheckedChildren" type="close" />
        </a-switch>
      </template>
      <template slot="queryType" slot-scope="text, record">
        <a-select style="width: 100px" v-model="record.queryType" :disabled="!record.queryParameters">
          <a-select-option v-for="(item, index) in codeGenQueryTypeData" :key="index" :value="item.value">{{
            item.name
          }}</a-select-option>
        </a-select>
      </template>
    </a-table>
  </a-card>
</template>
<script>
import { sysDict } from '@/api/system'
export default {
  components: {

  },
  data () {
    return {
      // 表头
      columns: columns,
      codeGenDetailShow: false,
      tableLoading: false,
      visible: false,
      loadData: [],
      javaTypeData: [],
      frontendTypeData: [],
      dropDownCodeData: [],
      codeGenQueryTypeData: [],
      yesOrNoData: []
    }
  },
  methods: {
    /**
     * 打开界面
     */
    detail (data) {
      this.codeGenDetailShow = true
      this.dropDownData()
      const params = {
        masterId: data.id
      }
      this.$http.get('/sysCodeGen', { params: params }).then(res => {
        if (res.code === 200) {
          this.loadData = res.result
        }
      }).finally(() => {
        this.tableLoading = false
      })
    },
    dropDownData () {
      this.$http.get('/sysDic/GetMasterList').then(res => {
        if (res.code === 200) {
          this.dropDownCodeData = res.result
        }
      })
      sysDict({ code: 'codeGenFrontendType' }).then((res) => {
        if (res.code === 200) {
          this.frontendTypeData = res.result
        }
      })
      sysDict({ code: 'codeGenQueryType' }).then((res) => {
        if (res.code === 200) {
          this.codeGenQueryTypeData = res.result
        }
      })
    },
    frontendTypeChange (record) {
      record.dropDownCode = ''
    },
    /**
     * 提交表单
     */
    handleSubmit () {
      this.tableLoading = true
      const paramas = { detail: this.loadData }
      this.$http.put('/sysCodeGen/detail', paramas).then(res => {
        this.tableLoading = false
        if (res.code === 200) {
          this.$message.success('保存成功')
        }
      }).finally(() => {
        this.tableLoading = false
      })
    },
    filterOption (input, option) {
      return (
        option.componentOptions.children[0].text.toLowerCase().indexOf(input.toLowerCase()) >= 0 || option.componentOptions.children[0].value.toLowerCase().indexOf(input.toLowerCase()) >= 0
      )
    },
    /**
     * 判断是否（用于是否能选择或输入等）
     */
    judgeColumns (data) {
      if (
        data.columnName.indexOf('createdUserName') > -1 ||
        data.columnName.indexOf('createdTime') > -1 ||
        data.columnName.indexOf('updatedUserName') > -1 ||
        data.columnName.indexOf('updatedTime') > -1 ||
        data.columnKey === 'True'
      ) {
        return true
      }
      return false
    },
    handleCancel () {
      this.$emit('ok')
      this.loadData = []
      this.codeGenDetailShow = false
    }
  }
}
const columns = [{
  title: '字段',
  dataIndex: 'dbColumnName'
},
{
  slots: { title: 'columnDescriptionTitle' },
  dataIndex: 'columnDescription',
  scopedSlots: {
    customRender: 'columnDescription'
  }
},
{
  title: '类型',
  dataIndex: 'dataType'
},
{
  title: '作用类型',
  dataIndex: 'frontendType',
  scopedSlots: {
    customRender: 'frontendType'
  }
},
{
  title: '字典',
  dataIndex: 'dropDownCode',
  scopedSlots: {
    customRender: 'dropDownCode'
  }
},
{
  title: '必填',
  align: 'center',
  dataIndex: 'required',
  scopedSlots: {
    customRender: 'required'
  }
},
{
  align: 'center',
  dataIndex: 'queryParameters',
  slots: { title: 'queryParametersTitle' },
  scopedSlots: {
    customRender: 'queryParameters'
  }
},
{
  title: '查询方式',
  dataIndex: 'queryType',
  scopedSlots: {
    customRender: 'queryType'
  }
},
{
  title: '排序',
  dataIndex: 'order',
  scopedSlots: {
    customRender: 'order'
  }
}
]
</script>
