<template>
  <a-card :bordered="false">
    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="8" :sm="24">
            <a-form-item label="角色名称">
              <a-input placeholder="请输入" v-model="queryParam.roleName" />
            </a-form-item>
          </a-col>
          <a-col :md="8" :sm="24">
            <a-form-item label="状态">
              <a-select placeholder="请选择" default-value="" v-model="queryParam.enable">
                <a-select-option value="">全部</a-select-option>
                <a-select-option value="true">正常</a-select-option>
                <a-select-option value="false">禁用</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="8" :sm="24">
            <span class="table-page-search-submitButtons">
              <a-button type="primary" @click="$refs.table.refresh(true)">查询</a-button>
              <a-button style="margin-left: 8px">重置</a-button>
            </span>
          </a-col>
        </a-row>
      </a-form>
    </div>
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="$refs.detailModal.detail()">新建角色</a-button>
    </div>
    <s-table ref="table" size="default" :columns="columns" :data="loadData" rowKey="id">
      <div slot="expandedRowRender" slot-scope="record" style="margin: 0">
        <a-row :gutter="24" :style="{ marginBottom: '12px' }">
          <a-col :span="12" v-for="(role, index) in record.permissions" :key="index" :style="{ marginBottom: '12px' }">
            <a-col :span="4">
              <span>{{ role.permissionName }}：</span>
            </a-col>
            <a-col :span="20" v-if="role.actionEntitySet.length > 0">
              <a-tag color="cyan" v-for="(action, k) in role.actionEntitySet" :key="k">{{ action.describe }}</a-tag>
            </a-col>
            <a-col :span="20" v-else>-</a-col>
          </a-col>
        </a-row>
      </div>
      <span slot="action" slot-scope="text, record">
        <a @click="$refs.detailModal.detail(record)">编辑</a>
        <a-divider type="vertical" />
        <a-popconfirm title="是否要删除此行？" @confirm="remove(record.id)">
          <a>删除</a>
        </a-popconfirm>
        <a-divider type="vertical" />
        <a-dropdown>
          <a class="ant-dropdown-link">
            更多 <a-icon type="down" />
          </a>
          <a-menu slot="overlay">
            <a-menu-item>
              <a href="javascript:;" @click="$refs.roleMenuModal.create(record.id)">授权菜单</a>
            </a-menu-item>
            <a-menu-item>
              <a href="javascript:;" @click="$refs.roleMenuPermsModal.create(record.id)">授权菜单按钮</a>
            </a-menu-item>
          </a-menu>
        </a-dropdown>
      </span>
      <span slot="enable" slot-scope="text" style="margin-left: -13px;">
        <a-tag :color="text ? 'green' : 'red'">{{ text | enableFilter }}</a-tag>
      </span>
    </s-table>

    <role-modal ref="detailModal" @ok="handleOk" />
    <role-menu-modal ref="roleMenuModal" @ok="handleOk" />
    <role-menu-perms-modal ref="roleMenuPermsModal" @ok="handleOk" />
    <!-- <a-button v-action:add123>添加用户</a-button> -->

  </a-card>
</template>

<script>
import { STable } from '@/components'
import RoleModal from './modules/RoleModal'
import RoleMenuModal from './modules/RoleMenuModal'
import RoleMenuPermsModal from './modules/RoleMenuPermsModal'
const enableMap = {
  true: {
    text: '启用'
  },
  false: {
    text: '禁用'
  }
}
export default {
  name: 'TableList',
  components: {
    STable,
    RoleModal,
    RoleMenuModal,
    RoleMenuPermsModal
  },
  data() {
    return {
      description: '列表使用场景：后台管理中的权限管理以及角色管理，可用于基于 RBAC 设计的角色权限控制，颗粒度细到每一个操作类型。',
      visible: false,
      form: null,
      mdl: {},
      // 高级搜索 展开/关闭
      advanced: false,
      // 查询参数
      queryParam: { enable: '' },
      // 表头
      columns: [
        {
          title: '角色名称',
          dataIndex: 'roleName'
        },
        {
          title: '描述',
          dataIndex: 'describe'
        },
        {
          title: '启用状态',
          dataIndex: 'enable',
          scopedSlots: { customRender: 'enable' }
        },
        {
          title: '操作',
          width: '200px',
          dataIndex: 'action',
          scopedSlots: { customRender: 'action' }
        }
      ],
      // 加载数据方法 必须为 Promise 对象
      loadData: parameter => {
        return this.$http.get('/SysRole/Page', {
          params: Object.assign(parameter, this.queryParam)
        }).then(res => {
          return res.result
        })
      },

      selectedRowKeys: [],
      selectedRows: []
    }
  },
  methods: {
    handleEdit(record) {
      this.mdl = Object.assign({}, record)

      this.mdl.permissions.forEach(permission => {
        permission.actionsOptions = permission.actionEntitySet.map(action => {
          return { label: action.describe, value: action.action, defaultCheck: action.defaultCheck }
        })
      })

      console.log(this.mdl)
      this.visible = true
    },
    handleOk() {
      // 新增/修改 成功时，重载列表
      this.$refs.table.refresh()
    },
    remove(key) {
      this.$http.delete('/sysRole', { data: { Id: key } }).then(res => {
        if (res.code === 200) {
          this.$message.success('删除成功')
          this.handleOk()
        }
      })
    },
    onChange(selectedRowKeys, selectedRows) {
      this.selectedRowKeys = selectedRowKeys
      this.selectedRows = selectedRows
    },
    toggleAdvanced() {
      this.advanced = !this.advanced
    }
  },
  watch: {
    /*
      'selectedRows': function (selectedRows) {
        this.needTotalList = this.needTotalList.map(item => {
          return {
            ...item,
            total: selectedRows.reduce( (sum, val) => {
              return sum + val[item.dataIndex]
            }, 0)
          }
        })
      }
      */
  },
  filters: {
    enableFilter(type) {
      return enableMap[type].text
    },

  },
}
</script>
